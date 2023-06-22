using GamingApi.Games.Domain;
using GamingApi.Games.DTOs;
using GamingApi.SharedKernel.Mapping;

namespace GamingApi.Games.Mappers;


[Mapper]
public partial class SteamGame2GameDtoMapper : Mapper<SteamGame, GameDto>
{

    [MapProperty(nameof(SteamGame.AppId), nameof(GameDto.Id))]
    public partial GameDto Map(SteamGame source);

    public static Dictionary<string, bool> Dictionary2PlatformDto(Platforms platform)
    {
        var dictionary = new Dictionary<string, bool>
        {
            { nameof(Platforms.Linux), platform.Linux },
            { nameof(Platforms.Windows), platform.Windows },
            { nameof(Platforms.Mac), platform.Mac }
        };

        return dictionary;
    }
}

