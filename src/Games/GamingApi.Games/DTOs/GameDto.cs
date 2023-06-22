namespace GamingApi.Games.DTOs;

public record GameDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ShortDescription { get; init; } = string.Empty;
    public string Publisher { get; init; } = string.Empty;
    public string Genre { get; init; } = string.Empty;
    public List<string> Categories { get; init; } = new List<string>();
    public PlatformDto Platforms { get; init; } = new PlatformDto();
    public DateTime ReleaseDate { get; init; }
    public int RequiredAge { get; init; }
}

public record PlatformDto
{
    public bool Windows { get; init; }
    public bool Mac { get; init; }
    public bool Linux { get; init; }
}
