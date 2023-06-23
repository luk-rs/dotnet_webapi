using System.Text.Json;
using FluentAssertions;
using GamingApi.Games.DTOs;

namespace GamingApi.WebApi.Specflow.Steps.HappyPath
{
    [Binding]
    public class ValidRequestsSteps
    {
        private readonly ScenarioContext _context;

        public ValidRequestsSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then(@"the responses contain valid game dtos")]
        public async Task ThenTheResponsesContainValidGameDtosAsync()
        {
            static async Task<GameDto[]?> deserialization_promise(HttpResponseMessage message)
            {
                var content = await message.Content.ReadAsStringAsync();
                var gameDto = JsonSerializer.Deserialize<GameDto[]>(content);
                return gameDto;
            }

            var responses = _context.Get<HttpResponseMessage[]>();

            var deserialization = () => Task.WhenAll(responses.Select(deserialization_promise).ToArray());

            var validation = await deserialization.Should().NotThrowAsync<JsonException>();

            var results = await validation.And.Subject();

            results.Should().NotBeNullOrEmpty();
            results.Should().HaveCount(responses.Length);
            results.Should().NotContainNulls();
            results.Select(dto => dto).Should().NotContainNulls();
        }
    }
}
