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

        public async Task<PagedResult<IReadOnlyList<GameReview>>> GetAllPagedAsync(int page, int pageSize, int homeTeamId, int visitorTeamId, string date)
        {
            var reviews = await context.GameReviews
                .Include(x => x.Fan)
                .OrderByDescending(x => x.Date)
                .Where(x => x.HomeTeamId == homeTeamId && x.VisitorTeamId == visitorTeamId && x.Date == date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await context.GameReviews.CountAsync();

            return PagedResult<IReadOnlyList<GameReview>>.Success(reviews, totalCount);
        }

        public async Task<PagedResult<IReadOnlyList<GameReview>>> GetAllPagedByFanIdAsync(int page, int pageSize, string fanId)
        {
            var reviews = await context.GameReviews
                .Include(x => x.Fan)
                .OrderByDescending(x => x.Date)
                .Where(x => x.FanId == fanId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await context.GameReviews.CountAsync();

            return PagedResult<IReadOnlyList<GameReview>>.Success(reviews, totalCount);
        }

        public async Task<Result<IReadOnlyList<GameReview>>> FindByDateIncludingAll(string date)
        {
            var groupedGameReviews = await context.GameReviews
                .Include(x => x.Fan)
                .Where(x => x.Date == date)
                .GroupBy(x => new { x.VisitorTeamId, x.HomeTeamId, x.Date })
                .Select(g => g.First())
                .ToListAsync();

            return Result<IReadOnlyList<GameReview>>.Success(groupedGameReviews);
        }

    }
}
