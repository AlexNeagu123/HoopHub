using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class PlayerPerformanceReviewRepository(UserFeaturesContext context) : BaseRepository<PlayerPerformanceReview>(context), IPlayerPerformanceReviewRepository
    {
        public async Task<Result<PlayerPerformanceReview>> FindByIdAsyncIncludingAll(int homeTeamId, int visitorTeamId, Guid playerId, string date, string fanId)
        {
            var review = await context.PlayerPerformanceReviews
                .Include(x => x.Fan)
                .FirstOrDefaultAsync(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.PlayerId == playerId && x.Date == date && x.FanId == fanId);

            return review == null
                ? Result<PlayerPerformanceReview>.Failure("Player performance review not found")
                : Result<PlayerPerformanceReview>.Success(review);
        }

        public async Task<decimal?> GetAverageRatingByGameTupleId(int homeTeamId, int visitorTeamId, Guid playerId, string date)
        {
            var reviewCount = await context.PlayerPerformanceReviews
                .CountAsync(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.PlayerId == playerId && x.Date == date);

            decimal? averageRating = null;

            if (reviewCount > 0)
            {
                averageRating = await context.PlayerPerformanceReviews
                    .Where(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.PlayerId == playerId && x.Date == date)
                    .AverageAsync(x => x.Rating);
            }

            return averageRating;
        }

        public async Task<Result<IReadOnlyList<PlayerPerformanceReview>>> GetAllAveragePerformancesByGameAsync(int homeTeamId, int visitorTeamId, string date)
        {
            var reviews = await context.PlayerPerformanceReviews
                .Include(x => x.Fan)
                .Where(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.Date == date)
                .GroupBy(x => new { x.PlayerId, x.VisitorTeamId, x.HomeTeamId, x.Date })
                .Select(g => g.First())
                .ToListAsync();

            return Result<IReadOnlyList<PlayerPerformanceReview>>.Success(reviews);
        }

        

        public async Task<decimal?> GetOwnRatingByTupleId(int homeTeamId, int visitorTeamId, Guid playerId, string date, string fanId)
        {
            var review = await context.PlayerPerformanceReviews
                .FirstOrDefaultAsync(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.PlayerId == playerId && x.Date == date && x.FanId == fanId);

            return review?.Rating;
        }

        public async Task<decimal?> GetAverageRatingByPlayerId(Guid playerId, decimal? newRating = null)
        {
            var reviewCount = await context.PlayerPerformanceReviews
                .CountAsync(x => x.PlayerId == playerId);

            if (reviewCount <= 0)
                return newRating;
            
            var totalRating = await context.PlayerPerformanceReviews
                    .Where(x => x.PlayerId == playerId)
                    .SumAsync(x => x.Rating);

            
            return newRating.HasValue ? (totalRating + newRating.Value) / (reviewCount + 1) : totalRating / reviewCount;
        }
    }
}
