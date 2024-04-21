using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Reviews;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IPlayerPerformanceReviewRepository : IAsyncRepository<PlayerPerformanceReview>
    {
        Task<Result<PlayerPerformanceReview>> FindByIdAsyncIncludingAll(Guid homeTeamId, Guid visitorTeamId, Guid playerId, string date, string fanId);
    }
}
