using System.Text.Json.Serialization;

namespace GamingApi.Consumer.Contracts;
internal sealed class GamesContract
{
    [JsonPropertyName("items")]
    public GameDto[] Items { get; init; } = Array.Empty<GameDto>();

    [JsonPropertyName("totalItems")]
    public int TotalItems { get; init; }
}
