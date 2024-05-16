using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class GameReviewRepository(UserFeaturesContext context) : BaseRepository<GameReview>(context), IGameReviewRepository
    {
        public async Task<Result<GameReview>> FindByIdAsyncIncludingAll(int homeTeamId, int visitorTeamId, string date, string fanId)
        {
            var gameReview = await context.GameReviews
                .Include(x => x.Fan)
                .FirstOrDefaultAsync(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.Date == date && x.FanId == fanId);

            return gameReview == null
                ? Result<GameReview>.Failure("Game review not found")
                : Result<GameReview>.Success(gameReview);
        }

        public async Task<decimal?> GetAverageRatingByGameTupleId(int homeTeamId, int visitorTeamId, string date)
        {
            var reviewCount = await context.GameReviews
                .CountAsync(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.Date == date);

            decimal? averageRating = null;

            if (reviewCount > 0)
            {
                averageRating = await context.GameReviews
                    .Where(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.Date == date)
                    .AverageAsync(x => x.Rating);
            }

            return averageRating;
        }
    }
}
