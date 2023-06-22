using GamingApi.Games.Domain;
using GamingApi.Games.DTOs;
using GamingApi.Games.Mappers;
using GamingApi.Tests.SharedKernel.Attributes;

namespace GamingApi.Games.xUnit.Mappers;

public class MapperTests
{
    [Theory, AutoNSubstituteData]
    public void MapSteamGame2GameDto(SteamGame2GameDtoMapper sut, SteamGame game)
    {
        // todo this should probabaly be improved with a specimen builder or something like that
        void replace(int idx, string newKey)
        {
            var pair = game.Platforms.ElementAt(idx);
            game.Platforms.Remove(pair.Key);
            game.Platforms[newKey.ToLower()] = pair.Value;
        };

        replace(0, nameof(PlatformDto.Windows));
        replace(1, nameof(PlatformDto.Mac));
        replace(2, nameof(PlatformDto.Linux));

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
