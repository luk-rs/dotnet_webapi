namespace GamingApi.SharedKernel.IoC;
public static class ServiceCollectionScanExtensions
{
    public static IServiceCollection ScanAsSelf<T>(this IServiceCollection services, Assembly[] assemblies) => services.ScanAsSelf(assemblies, typeof(T));
    public static IServiceCollection ScanAsSelf(this IServiceCollection services, Assembly[] assemblies, Type typeToScan)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeToScan))
            .AsSelf()
            .WithTransientLifetime());

        return services;
    }
}
