using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class ThreadCommentRepository(UserFeaturesContext context) : BaseRepository<ThreadComment>(context), IThreadCommentRepository
    {
        public async Task<Result<ThreadComment>> FindByIdAsyncIncludingAll(Guid commentId)
        {
            var comment = await context.Comments
                .Include(c => c.Fan)
                .Include(c => c.GameThread)
                .Include(c => c.TeamThread)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            return comment == null ? Result<ThreadComment>.Failure($"Comment with Id {commentId} not found") : Result<ThreadComment>.Success(comment);
        }
    }
}
