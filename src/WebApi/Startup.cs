using System.Reflection;
using System.Text.Json;
using GamingApi.SharedKernel.IoC;
using GamingApi.SharedKernel.Mapping;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Yld.GamingApi.WebApi.Middlewares;

namespace Yld.GamingApi.WebApi;

public sealed class Startup
{

    private static readonly DirectoryInfo _executingPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory!;
    private static readonly Assembly[] _mediatRAssemblies = (new[]
        {
            // btw very strange pattern to have an assembly name different from project name
            // why just not change the project name itself ?
            "Yld.GamingApi.WebApi",
            "GamingApi.Games"
            // "add more assemblies here"
        })
        .Select(assemblyName =>
        {
            var assemblyFile = Path.Combine(_executingPath.FullName, $"{assemblyName}.dll");
            return Assembly.LoadFrom(assemblyFile);
        })
        .ToArray();

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Yld Gaming API", Version = "v1" });
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "YldGamingApi.xml"));
        });

        services.AddHttpContextAccessor();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(_mediatRAssemblies));

        services.AddHttpClient("yld.gamesfeed", httpClient => httpClient.BaseAddress = new Uri("https://yld-recruitment-resources.s3.eu-west-2.amazonaws.com"));


        services.ScanAsSelf<IMiddleware>(_mediatRAssemblies);

        services.ScanAsSelf(_mediatRAssemblies, typeof(Mapper<,>));

    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yld Gaming API v1");
            c.DocExpansion(DocExpansion.List);
        });

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionFormatterMiddleware>();

        app.UseRouting();

        app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
