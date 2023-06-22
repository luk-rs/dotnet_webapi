namespace GamingApi.Games.Domain;
public sealed record SteamGame
{
    public int AppId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ShortDescription { get; init; } = string.Empty;
    public string Developer { get; init; } = string.Empty;
    public string Publisher { get; init; } = string.Empty;
    public string Genre { get; init; } = string.Empty;
    public Dictionary<string, int> Tags { get; init; } = new();
    public string Type { get; init; } = string.Empty;
    public List<string> Categories { get; init; } = new List<string>();
    public string Owners { get; init; } = string.Empty;
    public int Positive { get; init; }
    public int Negative { get; init; }
    public string Price { get; init; } = string.Empty;
    public string InitialPrice { get; init; } = string.Empty;
    public string Discount { get; init; } = string.Empty;
    public int CCU { get; init; }
    public string Languages { get; init; } = string.Empty;
    public Platforms Platforms { get; init; } = new();

    [JsonPropertyName("release_date")]
    public DateTime ReleaseDate { get; init; }
    public int RequiredAge { get; init; }
    public string Website { get; init; } = string.Empty;
    public string HeaderImage { get; init; } = string.Empty;
}
