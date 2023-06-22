using System.Text.Json;
using System.Text.RegularExpressions;
using GamingApi.Games.Domain;
using GamingApi.Games.DTOs;
using GamingApi.Games.Mappers;
using MediatR;

namespace GamingApi.Games.CQ;

public sealed record GetGamesQuery(int Limit, int Offset) : IRequest<GameDto[]>;

public sealed class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, GameDto[]>
{
    private const string _yldGamesFeedEndpoint = @"steam_games_feed.json";
    private const string _elemsPattern = @"\{(?:[^{}]|(?<open>{)|(?<close-open>}))+(?(open)(?!))\}";
    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;
    private readonly SteamGame2GameDtoMapper _mapper;

    public GetGamesQueryHandler(IHttpClientFactory factory, SteamGame2GameDtoMapper mapper)
    {
        _http = factory.CreateClient("yld.gamesfeed");
        _mapper = mapper;
    }

    public async Task<GameDto[]> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var response = await _http.GetAsync(_yldGamesFeedEndpoint, cancellationToken);
        var data = await response.Content.ReadAsStringAsync(cancellationToken);
        var matches = Regex.Matches(data, _elemsPattern);


        var steamGames = new List<SteamGame>();
        var startIndex = request.Offset;
        var endIndex = startIndex + (request.Limit);
        for (var i = startIndex; i < endIndex && i < matches.Count; i++)
        {
            var match = matches[i];
            var game = JsonSerializer.Deserialize<SteamGame>(match.Value, _jsonOpts);
            steamGames.Add(game ?? throw new Exception("error while deserializing games"));
        }


        var games = steamGames.Select(game => _mapper.Map(game)).ToArray();


        return games;
    }
}
