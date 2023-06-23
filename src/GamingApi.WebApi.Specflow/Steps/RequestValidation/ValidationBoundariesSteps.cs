using System.Net;
using FluentAssertions;

namespace GamingApi.WebApi.Specflow.Steps.RequestValidation;

[Binding]
internal class ValidationBoundariesSteps
{
    private readonly ScenarioContext _context;

    public ValidationBoundariesSteps(ScenarioContext context)
    {
        _context = context;
    }

    [When(@"a GET request is made to ""(.*)"" with the following parameters")]
    public async Task WhenAGETRequestIsMadeToWithTheFollowingParametersAsync(string endpoint, Table parametersTable)
    {

        var responses = new HttpResponseMessage[parametersTable.RowCount];

        var nRow = 0;
        foreach (var row in parametersTable.Rows)
        {
            var limit = row["limit"];
            var offset = row["offset"];

            var client = _context.Get<HttpClient>();

            var url = $"{endpoint}?offset={offset}&limit={limit}";
            var response = await client.GetAsync(url);
            responses[nRow++] = response;
        }

        _context.Set(() => responses);
    }

    [Then(@"the API should respond with the following status codes")]
    public void ThenTheAPIShouldRespondWithTheFollowingStatusCodes(Table statusCodeTable)
    {
        var expectedCodes = new HttpStatusCode[statusCodeTable.RowCount];

        var nRow = 0;
        foreach (var row in statusCodeTable.Rows)
        {
            if (!Enum.TryParse<HttpStatusCode>(row["status code"], out var statusCode))
                throw new Exception("check step definition or feature values, cannot parse status code!");

            expectedCodes[nRow++] = statusCode;
        }


        var responses = _context.Get<HttpResponseMessage[]>();
        var responseCodes = responses.Select(x => x.StatusCode).ToArray();

        responseCodes.Should().BeEquivalentTo(expectedCodes);

    }
}
