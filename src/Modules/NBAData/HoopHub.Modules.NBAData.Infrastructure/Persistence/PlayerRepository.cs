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
                .Where(p => p.CurrentTeamId == teamId)
                .ToListAsync();
            return Result<IReadOnlyList<Player>>.Success(players);
        }

        public async Task<Result<Player>> FindByApiIdAsync(int apiId)
        {
            var player = await context.Set<Player>()
                .FirstOrDefaultAsync(p => p.ApiId == apiId);
            return player == null ? Result<Player>.Failure($"Entity with ApiId {apiId} not found") : Result<Player>.Success(player);
        }

        public async Task<Result<IReadOnlyList<Player>>> GetAllActivePlayers()
        {
            var players = await context.Set<Player>()
                .Where(p => p.IsActive)
                .ToListAsync();
            return Result<IReadOnlyList<Player>>.Success(players);
        }
    }
}
