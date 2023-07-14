using System.Text.Json.Serialization;

namespace GamingApi.Consumer.Contracts;

internal sealed class GameDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("shortDescription")]
    public string ShortDescription { get; init; } = string.Empty;

    [JsonPropertyName("publisher")]
    public string Publisher { get; init; } = string.Empty;

    [JsonPropertyName("genre")]
    public string Genre { get; init; } = string.Empty;

    [JsonPropertyName("categories")]
    public List<string> Categories { get; init; } = new List<string>();

    [JsonPropertyName("platforms")]
    public Dictionary<string, bool> Platforms { get; init; } = new();

    [JsonPropertyName("releaseDate")]
    public DateTime ReleaseDate { get; init; }

    [JsonPropertyName("requiredAge")]
    public int RequiredAge { get; init; }
}
