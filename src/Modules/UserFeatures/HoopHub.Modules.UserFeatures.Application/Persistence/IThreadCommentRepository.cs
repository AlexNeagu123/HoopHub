using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IThreadCommentRepository : IAsyncRepository<ThreadComment>
    {
        Task<Result<ThreadComment>> FindByIdAsyncIncludingAll(Guid commentId);
        Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByTeamThreadAsync(Guid teamThreadId, int page, int pageSize);

        Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByGameThreadMostPopularAsync(Guid gameThreadId,
            int page, int pageSize);
        Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByGameThreadAsync(Guid gameThreadId, int page, int pageSize);

        Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByTeamThreadMostPopularAsync(Guid teamThreadId,
            int page, int pageSize);

        Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByFanAsync(string fanId, int page,
            int pageSize);

        Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByFanMostPopularAsync(string fanId, int page,
            int pageSize);
    }
}
