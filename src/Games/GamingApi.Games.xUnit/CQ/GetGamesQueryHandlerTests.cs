using System.Net;
using FluentValidation;
using GamingApi.Games.CQ;
using GamingApi.Games.Domain;
using GamingApi.Games.Mappers;


namespace GamingApi.Games.xUnit.CQ;


public sealed class GetGamesQueryHandlerFixture
{
    private readonly IHttpClientFactory _factory;
    private readonly SteamGame2GameDtoMapper _mapper;
    private readonly MockHttpMessageHandler _handler;

    public GetGamesQueryHandlerFixture(
        IHttpClientFactory factory,
        SteamGame2GameDtoMapper mapper,
        MockHttpMessageHandler handler)
    {
        _factory = factory;
        _mapper = mapper;
        _handler = handler;
    }

    internal GetGamesQueryHandler GenerateSut()
    {
        var client = _handler.ToHttpClient();
        client.BaseAddress = new Uri(Connections.SteamGamesFeed.BaseUrl);

        _factory.CreateClient(Arg.Is(Connections.SteamGamesFeed.Name)).Returns(_ => client);

        return new GetGamesQueryHandler(_factory, _mapper);
    }

    internal void Listen(HttpMethod method, string url, Action<MockedRequest> configure)
    {
        var mocked = _handler.When(method, url);

        configure(mocked);
    }

    private JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };
    internal string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, _serializerOptions);
}

public sealed class GetGamesQueryHandlerTests
{
    [Theory]
    [InlineAutoNSubstituteData(0, 0, 0)]
    [InlineAutoNSubstituteData(1, 3, 2)]
    [InlineAutoNSubstituteData(0, 3, 3)]
    [InlineAutoNSubstituteData(1, 2, 2)]
    public async Task CorrectlyPagesTheResults(int offset, int limit, int expected, GetGamesQueryHandlerFixture fixture, SteamGame[] games)
    {
        fixture.Listen(
            HttpMethod.Get, $@"{Connections.SteamGamesFeed.BaseUrl}/{Connections.SteamGamesFeed.Path}",
            mocked => mocked.Respond(new StringContent(fixture.Serialize(games), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json))
        );

        var sut = fixture.GenerateSut();

        var dtos = await sut.Handle(new GetGamesQuery(limit, offset), new CancellationToken());

        dtos.Length.Should().Be(expected);
        dtos.Select(d => d.Id).Should().BeEquivalentTo(games.Skip(offset).Take(limit).Select(g => g.AppId));
    }

    [Theory, AutoNSubstituteData]
    public async Task ThrowsOnBadRequestToThirdParty(GetGamesQueryHandlerFixture fixture, int offset, int limit)
    {
        fixture.Listen(
            HttpMethod.Get, $@"{Connections.SteamGamesFeed.BaseUrl}/{Connections.SteamGamesFeed.Path}",
            mocked => mocked.Respond(HttpStatusCode.BadGateway)
        );

        var sut = fixture.GenerateSut();

        var handling = async () => await sut.Handle(new GetGamesQuery(limit, offset), new CancellationToken());

        await handling.Should().ThrowAsync<Exception>();
    }
}
