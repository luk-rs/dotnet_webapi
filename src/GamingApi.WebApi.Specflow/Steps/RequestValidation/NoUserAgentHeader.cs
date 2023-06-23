namespace GamingApi.WebApi.Specflow.Steps.RequestValidation
{
    [Binding]
    public class NoUserAgentHeader
    {
        private readonly ScenarioContext _context;

        public NoUserAgentHeader(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"(.*) header is not configured as a default request header")]
        public void GivenUser_AgentHeaderIsNotConfiguredAsADefaultRequestHeader(string header)
        {
            var client = _context.Get<HttpClient>();

            client.DefaultRequestHeaders.Remove(header);

            _context.Set(() => client);

        }
    }
}
