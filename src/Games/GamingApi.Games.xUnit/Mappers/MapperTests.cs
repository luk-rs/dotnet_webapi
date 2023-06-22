using GamingApi.Games.Domain;
using GamingApi.Games.DTOs;
using GamingApi.Games.Mappers;

namespace GamingApi.Games.xUnit.Mappers;

public sealed class MapperTests
{
    [Theory, AutoNSubstituteData]
    public void MapSteamGame2GameDto(SteamGame2GameDtoMapper sut, SteamGame game)
    {
        var dto = sut.Map(game);

        dto.Should().BeEquivalentTo(
            game,
            opts => opts
            .ExcludingMissingMembers()
            .WithMapping<GameDto, SteamGame>(d => d.Id, g => g.AppId)
            .Excluding(d => d.Platforms)
        );

        dto.Platforms.Should().BeEquivalentTo(SteamGame2GameDtoMapper.Dictionary2PlatformDto(game.Platforms));
    }
}
