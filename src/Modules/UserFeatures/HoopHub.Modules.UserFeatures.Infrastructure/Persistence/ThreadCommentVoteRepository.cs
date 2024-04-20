using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class ThreadCommentVoteRepository(UserFeaturesContext context) : BaseRepository<CommentVote>(context), IThreadCommentVoteRepository
    {
        public async Task<Result<CommentVote>> FindByIdAsyncIncludingAll(Guid commentId, string fanId)
        {
            var threadVote = await context.CommentVotes
                .Include(x => x.ThreadComment)
                .Include(x => x.Fan)
                .FirstOrDefaultAsync(x => x.CommentId == commentId && x.FanId == fanId);

            return threadVote == null
                ? Result<CommentVote>.Failure("Comment vote not found")
                : Result<CommentVote>.Success(threadVote);
        }
    }
}
