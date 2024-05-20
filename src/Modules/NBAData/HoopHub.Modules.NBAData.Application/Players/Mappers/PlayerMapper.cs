using HoopHub.Modules.NBAData.Application.Players.Dtos;
using HoopHub.Modules.NBAData.Domain.Players;
using Riok.Mapperly.Abstractions;

namespace HoopHub.Modules.NBAData.Application.Players.Mappers
{
    [Mapper]
    public partial class PlayerMapper
    {
        public partial PlayerDto PlayerToPlayerDto(Player player);
    }
}
