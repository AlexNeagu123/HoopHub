using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class GameThreadRepository(UserFeaturesContext context) : BaseRepository<GameThread>(context), IGameThreadRepository
    {
        public async Task<Result<GameThread>> FindByTupleIdAsync(int homeTeamId, int visitorTeamId, string date)
        {
            var gameThread = await context.GameThreads.FirstOrDefaultAsync(x => x.HomeTeamApiId == homeTeamId && x.VisitorTeamApiId == visitorTeamId && x.Date == date);
            return gameThread == null ? Result<GameThread>.Failure("Game thread not found.") : Result<GameThread>.Success(gameThread);
        }

        public async Task<Result<IReadOnlyList<GameThread>>> GetAllByDateAsync(string date)
        {
            var gameThreads = await context.GameThreads.Where(x => x.Date == date).ToListAsync();
            return Result<IReadOnlyList<GameThread>>.Success(gameThreads);
        }
    }
}
