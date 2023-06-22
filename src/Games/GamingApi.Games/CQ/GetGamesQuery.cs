using GamingApi.Games.Domain;
using GamingApi.Games.DTOs;
using GamingApi.Games.Mappers;

namespace GamingApi.Games.CQ;

public sealed record GetGamesQuery(int Limit, int Offset) : IRequest<GameDto[]>;

public sealed class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, GameDto[]>
{
    private const string _elemsPattern = @"\{(?:[^{}]|(?<open>{)|(?<close-open>}))+(?(open)(?!))\}";
    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;
    private readonly SteamGame2GameDtoMapper _mapper;

    public GetGamesQueryHandler(IHttpClientFactory factory, SteamGame2GameDtoMapper mapper)
    {
        _http = factory.CreateClient(Connections.SteamGamesFeed.Name);
        _mapper = mapper;
    }

    public async Task<GameDto[]> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var response = await _http.GetAsync(Connections.SteamGamesFeed.Path, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Cannot connect to third party to retrieve the games feed. Status code of request '{response.StatusCode}'");

        var data = await response.Content.ReadAsStringAsync(cancellationToken);
        var matches = Regex.Matches(data, _elemsPattern);


        var steamGames = new List<SteamGame>();
        for (
            int startIndex = request.Offset, endIndex = startIndex + request.Limit, i = startIndex;
            i < endIndex && i < matches.Count;
            i++
        )
        {
            var match = matches[i];
            var game = JsonSerializer.Deserialize<SteamGame>(match.Value, _jsonOpts);
            steamGames.Add(game ?? throw new Exception("error while deserializing games"));
        }


        var games = steamGames.Select(game => _mapper.Map(game)).ToArray();

        return games;
    }
}
