using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface ITeamThreadVoteRepository : IAsyncRepository<TeamThreadVote>
    {
        Task<Result<TeamThreadVote>> FindByIdAsyncIncludingAll(Guid threadId, string fanId);
    }
}
