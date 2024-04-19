using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class TeamThreadRepository(UserFeaturesContext context) : BaseRepository<TeamThread>(context), ITeamThreadRepository
    {
        public async Task<PagedResult<List<TeamThread>>> GetByTeamIdPagedAsync(Guid teamId, int page, int pageSize)
        {
            var threads = await context.TeamThreads
                .Include(t => t.Fan)
                .Where(t => t.TeamId == teamId)
                .OrderByDescending(t => t.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var totalCount = await context.TeamThreads
                .Where(t => t.TeamId == teamId)
                .CountAsync();

            return PagedResult<List<TeamThread>>.Success(threads, totalCount);
        }

        public async Task<Result<TeamThread>> FindByIdAsyncIncludingFan(Guid id)
        {
            var thread = await context.TeamThreads
                .Include(t => t.Fan)
                .FirstOrDefaultAsync(t => t.Id == id);

            return thread == null
                ? Result<TeamThread>.Failure("Thread not found")
                : Result<TeamThread>.Success(thread);
        }
    }
}
