using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Follows;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface ITeamFollowEntryRepository : IAsyncRepository<TeamFollowEntry>
    {
        Task<Result<IReadOnlyList<TeamFollowEntry>>> GetAllByFanIdIncludingFanAsync(string fanId);
        Task<Result<IReadOnlyList<TeamFollowEntry>>> GetAllByTeamIdIncludingFansAsync(Guid teamId);
        Task<Result<TeamFollowEntry>> FindByIdPairIncludingFanAsync(string fanId, Guid teamId);
    }
}
