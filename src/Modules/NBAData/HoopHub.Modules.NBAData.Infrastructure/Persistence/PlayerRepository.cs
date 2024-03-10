using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Players;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class PlayerRepository(NBADataContext context) : BaseRepository<Player>(context), IPlayerRepository
    {
        public async Task<Result<IReadOnlyList<Player>>> GetAllActivePlayersByTeam(Guid teamId)
        {
            var players = await context.Set<Player>()
                .Where(p => p.TeamId == teamId)
                .Where(p => p.IsActive == true)
                .ToListAsync();
            return Result<IReadOnlyList<Player>>.Success(players);
        }
    }
}
