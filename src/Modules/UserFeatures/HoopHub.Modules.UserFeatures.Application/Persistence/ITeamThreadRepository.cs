using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface ITeamThreadRepository : IAsyncRepository<TeamThread>
    {
        Task<PagedResult<IReadOnlyList<TeamThread>>> GetByTeamIdPagedAsync(Guid teamId, int page, int pageSize);
        Task<Result<TeamThread>> FindByIdAsyncIncludingFan(Guid id);
        Task<PagedResult<IReadOnlyList<TeamThread>>> GetByTeamIdAndFanIdPagedAsync(Guid teamId, string fanId, int page, int pageSize);
    }
}
