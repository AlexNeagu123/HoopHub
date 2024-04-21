using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Follows;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IPlayerFollowEntryRepository : IAsyncRepository<PlayerFollowEntry>
    {
        Task<Result<IReadOnlyList<PlayerFollowEntry>>> GetAllByFanIdIncludingFanAsync(string fanId);
        Task<Result<IReadOnlyList<PlayerFollowEntry>>> GetAllByPlayerIdIncludingFansAsync(Guid playerId);
        Task<Result<PlayerFollowEntry>> FindByIdPairIncludingFanAsync(string fanId, Guid playerId);
    }
}
