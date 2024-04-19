using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface ITeamThreadRepository : IAsyncRepository<TeamThread>
    {
        Task<PagedResult<List<TeamThread>>> GetByTeamIdPagedAsync(Guid teamId, int page, int pageSize);
        Task<Result<TeamThread>> FindByIdAsyncIncludingFan(Guid id);
    }
}
