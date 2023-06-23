using Yld.GamingApi.WebApi;

namespace GamingApi.WebApi.Specflow.Steps.RequestValidation;

[Binding]
internal class InvalidInputsSteps
{
    private readonly ScenarioContext _context;

    public InvalidInputsSteps(ScenarioContext context)
    {
        _context = context;
    }

    [Given("the API is available")]
    public void GivenTheApiIsAvailable()
    {
        // No specific action needed for this step
        var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

        var testServer = new TestServer(webHostBuilder);
        var client = testServer.CreateClient();
        client.BaseAddress = testServer.BaseAddress;
        client.DefaultRequestHeaders.Add("User-Agent", "specflow");

        _context.Set(() => testServer);
        _context.Set(() => client ?? throw new Exception("could not instantiate client for the test server"));

    }
}
