namespace GamingApi.Games.Domain;

public sealed record Platforms
{
    public bool Windows { get; init; }
    public bool Linux { get; init; }
    public bool Mac { get; init; }
}
