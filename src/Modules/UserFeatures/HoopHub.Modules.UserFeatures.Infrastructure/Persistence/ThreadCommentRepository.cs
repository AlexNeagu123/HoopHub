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
                .Include(c => c.RespondsTo)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            return comment == null ? Result<ThreadComment>.Failure($"Comment with Id {commentId} not found") : Result<ThreadComment>.Success(comment);
        }

        public async Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByTeamThreadAsync(Guid teamThreadId, int page, int pageSize)
        {
            var comments = await context.Comments
                .Include(c => c.Fan)
                .Include(c => c.TeamThread)
                .Include(c => c.RespondsTo)
                .Where(c => c.TeamThreadId == teamThreadId)
                .Where(c => c.ParentId == null)
                .OrderByDescending(c => c.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await context.Comments.Where(c => c.TeamThreadId == teamThreadId).CountAsync();

            return PagedResult<IReadOnlyList<ThreadComment>>.Success(comments, totalCount);
        }

        public async Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByTeamThreadMostPopularAsync(Guid teamThreadId, int page, int pageSize)
        {
            var comments = await context.Comments
                .Include(c => c.Fan)
                .Include(c => c.TeamThread)
                .Include(c => c.RespondsTo)
                .Where(c => c.TeamThreadId == teamThreadId)
                .Where(c => c.ParentId == null)
                .OrderByDescending(c => c.UpVotes)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await context.Comments.Where(c => c.TeamThreadId == teamThreadId).CountAsync();

            return PagedResult<IReadOnlyList<ThreadComment>>.Success(comments, totalCount);
        }

        public async Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByGameThreadAsync(Guid gameThreadId, int page, int pageSize)
        {
            var comments = await context.Comments
                .Include(c => c.Fan)
                .Include(c => c.GameThread)
                .Include(c => c.RespondsTo)
                .Where(c => c.GameThreadId == gameThreadId)
                .Where(c => c.ParentId == null)
                .OrderByDescending(c => c.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await context.Comments.Where(c => c.GameThreadId == gameThreadId).CountAsync();

            return PagedResult<IReadOnlyList<ThreadComment>>.Success(comments, totalCount);
        }

        public async Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByGameThreadMostPopularAsync(Guid gameThreadId, int page, int pageSize)
        {
            var comments = await context.Comments
                .Include(c => c.Fan)
                .Include(c => c.GameThread)
                .Include(c => c.RespondsTo)
                .Where(c => c.GameThreadId == gameThreadId)
                .Where(c => c.ParentId == null)
                .OrderByDescending(c => c.UpVotes)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await context.Comments.Where(c => c.GameThreadId == gameThreadId).CountAsync();

            return PagedResult<IReadOnlyList<ThreadComment>>.Success(comments, totalCount);
        }

        public async Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByFanAsync(string fanId, int page, int pageSize)
        {
            var comments = await context.Comments
                .Include(c => c.Fan)
                .Include(c => c.GameThread)
                .Include(c => c.TeamThread)
                .Include(c => c.RespondsTo)
                .Where(c => c.FanId == fanId)
                .OrderByDescending(c => c.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await context.Comments.Where(c => c.FanId == fanId).CountAsync();

            return PagedResult<IReadOnlyList<ThreadComment>>.Success(comments, totalCount);
        }

        public async Task<PagedResult<IReadOnlyList<ThreadComment>>> GetPagedByFanMostPopularAsync(string fanId, int page, int pageSize)
        {
            var comments = await context.Comments
                  .Include(c => c.Fan)
                  .Include(c => c.GameThread)
                  .Include(c => c.TeamThread)
                  .Include(c => c.RespondsTo)
                  .Where(c => c.FanId == fanId)
                  .OrderByDescending(c => c.UpVotes)
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();

            var totalCount = await context.Comments.Where(c => c.FanId == fanId).CountAsync();

            return PagedResult<IReadOnlyList<ThreadComment>>.Success(comments, totalCount);
        }

        public async Task<Result<IReadOnlyList<ThreadComment>>> GetRepliesByComment(Guid commentId)
        {
            var replies = await context.Comments
                           .Include(c => c.Fan)
                           .Include(c => c.GameThread)
                           .Include(c => c.TeamThread)
                           .Include(c => c.RespondsTo)
                           .Where(c => c.ParentId == commentId)
                           .OrderBy(c => c.CreatedDate)
                           .ToListAsync();

            return Result<IReadOnlyList<ThreadComment>>.Success(replies);
        }
    }
}
