using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Reviews;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IPlayerPerformanceReviewRepository : IAsyncRepository<PlayerPerformanceReview>
    {
        Task<Result<PlayerPerformanceReview>> FindByIdAsyncIncludingAll(int homeTeamId, int visitorTeamId, Guid playerId, string date, string fanId);
        Task<decimal?> GetAverageRatingByGameTupleId(int homeTeamId, int visitorTeamId, Guid playerId, string date);
        Task<Result<IReadOnlyList<PlayerPerformanceReview>>> GetAllAveragePerformancesByGameAsync(int homeTeamId, int visitorTeamId,
            string date);

        Task<decimal?> GetOwnRatingByTupleId(int homeTeamId, int visitorTeamId, Guid playerId, string date, string fanId);

        Task<decimal?> GetAverageRatingByPlayerId(Guid playerId, decimal? newRating = null);
    }
}
