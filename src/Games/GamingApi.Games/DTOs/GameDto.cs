namespace GamingApi.Games.DTOs;

public sealed record GameDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ShortDescription { get; init; } = string.Empty;
    public string Publisher { get; init; } = string.Empty;
    public string Genre { get; init; } = string.Empty;
    public List<string> Categories { get; init; } = new List<string>();
    public Dictionary<string, bool> Platforms { get; init; } = new();
    public DateTime ReleaseDate { get; init; }
    public int RequiredAge { get; init; }
}

