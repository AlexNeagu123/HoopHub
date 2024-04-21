using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class PlayerPerformanceReviewRepository(UserFeaturesContext context) : BaseRepository<PlayerPerformanceReview>(context), IPlayerPerformanceReviewRepository
    {
        public async Task<Result<PlayerPerformanceReview>> FindByIdAsyncIncludingAll(Guid homeTeamId, Guid visitorTeamId, Guid playerId, string date, string fanId)
        {
            var review = await context.PlayerPerformanceReviews
                .Include(x => x.Fan)
                .FirstOrDefaultAsync(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.PlayerId == playerId && x.Date == date && x.FanId == fanId);

            return review == null
                ? Result<PlayerPerformanceReview>.Failure("Player performance review not found")
                : Result<PlayerPerformanceReview>.Success(review);
        }
    }
}
