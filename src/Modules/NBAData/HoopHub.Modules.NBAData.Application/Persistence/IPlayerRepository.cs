using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Players;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface IPlayerRepository : IAsyncRepository<Player>
    {
    }
}
