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
}
