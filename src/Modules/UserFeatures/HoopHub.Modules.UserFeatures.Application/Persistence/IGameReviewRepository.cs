using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Reviews;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IGameReviewRepository : IAsyncRepository<GameReview>
    {
        Task<Result<GameReview>> FindByIdAsyncIncludingAll(int homeTeamId, int visitorTeamId, string date, string fanId);
        Task<decimal?> GetAverageRatingByGameTupleId(int homeTeamId, int visitorTeamId, string date);
    }
}
