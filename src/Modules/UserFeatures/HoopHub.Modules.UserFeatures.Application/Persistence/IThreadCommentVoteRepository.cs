using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IThreadCommentVoteRepository : IAsyncRepository<CommentVote>
    {
        Task<Result<CommentVote>> FindByIdAsyncIncludingAll(Guid commentId, string fanId);
    }
}
