using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class GameReviewRepository(UserFeaturesContext context) : BaseRepository<GameReview>(context), IGameReviewRepository
    {
        public async Task<Result<GameReview>> FindByIdAsyncIncludingAll(Guid homeTeamId, Guid visitorTeamId, string date, string fanId)
        {
            var gameReview = await context.GameReviews
                .Include(x => x.Fan)
                .FirstOrDefaultAsync(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.Date == date && x.FanId == fanId);

            return gameReview == null
                ? Result<GameReview>.Failure("Game review not found")
                : Result<GameReview>.Success(gameReview);
        }
    }
}
