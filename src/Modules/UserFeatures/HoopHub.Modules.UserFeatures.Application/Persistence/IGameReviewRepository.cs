using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Reviews;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IGameReviewRepository : IAsyncRepository<GameReview>
    {
        Task<Result<GameReview>> FindByIdAsyncIncludingAll(Guid homeTeamId, Guid visitorTeamId, string date, string fanId);
    }
}
