using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class TeamThreadVoteRepository(UserFeaturesContext context) : BaseRepository<TeamThreadVote>(context), ITeamThreadVoteRepository
    {
        public async Task<Result<TeamThreadVote>> FindByIdAsyncIncludingAll(Guid threadId, string fanId)
        {
            var threadVote = await context.TeamThreadVotes
                .Include(x => x.TeamThread)
                .Include(x => x.Fan)
                .FirstOrDefaultAsync(x => x.TeamThreadId == threadId && x.FanId == fanId);

            return threadVote == null
                ? Result<TeamThreadVote>.Failure("Thread vote not found")
                : Result<TeamThreadVote>.Success(threadVote);

        }
    }
}
