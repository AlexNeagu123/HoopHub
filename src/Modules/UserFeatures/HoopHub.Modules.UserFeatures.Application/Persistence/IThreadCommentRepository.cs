using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IThreadCommentRepository : IAsyncRepository<ThreadComment>
    {
        Task<Result<ThreadComment>> FindByIdAsyncIncludingAll(Guid commentId);
    }
}
