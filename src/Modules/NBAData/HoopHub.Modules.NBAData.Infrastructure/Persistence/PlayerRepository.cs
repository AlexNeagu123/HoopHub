using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Players;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class PlayerRepository(NBADataContext dbContext) : BaseRepository<Player>(dbContext), IPlayerRepository;
}
