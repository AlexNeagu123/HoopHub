using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Follows;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class TeamFollowEntryRepository(UserFeaturesContext context)
        : BaseRepository<TeamFollowEntry>(context), ITeamFollowEntryRepository
    {
        public async Task<Result<IReadOnlyList<TeamFollowEntry>>> GetAllByFanIdIncludingFanAsync(string fanId)
        {
            var entries = await context.TeamFollowEntries
                .Include(x => x.Fan)
                .Where(x => x.FanId == fanId)
                .ToListAsync();

            return Result<IReadOnlyList<TeamFollowEntry>>.Success(entries);
        }

        public async Task<Result<IReadOnlyList<TeamFollowEntry>>> GetAllByTeamIdIncludingFansAsync(Guid teamId)
        {
            var entries = await context.TeamFollowEntries
                .Include(x => x.Fan)
                .Where(x => x.TeamId == teamId)
                .ToListAsync();

            return Result<IReadOnlyList<TeamFollowEntry>>.Success(entries);
        }

        public async Task<Result<TeamFollowEntry>> FindByIdPairIncludingFanAsync(string fanId, Guid teamId)
        {
            var entry = await context.TeamFollowEntries
                .Include(x => x.Fan)
                .FirstOrDefaultAsync(x => x.FanId == fanId && x.TeamId == teamId);

            return entry == null
                ? Result<TeamFollowEntry>.Failure("Team follow entry does not exist.")
                : Result<TeamFollowEntry>.Success(entry);
        }
    }
}
