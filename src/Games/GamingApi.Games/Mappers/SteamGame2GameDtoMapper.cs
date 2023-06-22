using GamingApi.Games.Domain;
using GamingApi.Games.DTOs;
using GamingApi.SharedKernel.Mapping;
using Riok.Mapperly.Abstractions;

namespace GamingApi.Games.Mappers;


[Mapper]
public partial class SteamGame2GameDtoMapper : Mapper<SteamGame, GameDto>
{

    [MapProperty(nameof(SteamGame.AppId), nameof(GameDto.Id))]
    public partial GameDto Map(SteamGame source);

    public static PlatformDto Dictionary2PlatformDto(Dictionary<string, bool> dictionary)
    {
        return new PlatformDto
        {
            Linux = dictionary[nameof(PlatformDto.Linux).ToLower()],
            Windows = dictionary[nameof(PlatformDto.Windows).ToLower()],
            Mac = dictionary[nameof(PlatformDto.Mac).ToLower()],
        };
    }
}
