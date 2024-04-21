using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Follows;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class PlayerFollowEntryRepository(UserFeaturesContext context) : BaseRepository<PlayerFollowEntry>(context), IPlayerFollowEntryRepository
    {
        public async Task<Result<IReadOnlyList<PlayerFollowEntry>>> GetAllByFanIdIncludingFanAsync(string fanId)
        {
            var entries = await context.PlayerFollowEntries
                .Include(x => x.Fan)
                .Where(x => x.FanId == fanId)
                .ToListAsync();

            return Result<IReadOnlyList<PlayerFollowEntry>>.Success(entries);
        }


        public async Task<Result<IReadOnlyList<PlayerFollowEntry>>> GetAllByPlayerIdIncludingFansAsync(Guid playerId)
        {
            var entries = await context.PlayerFollowEntries
                .Include(x => x.Fan)
                .Where(x => x.PlayerId == playerId)
                .ToListAsync();

            return Result<IReadOnlyList<PlayerFollowEntry>>.Success(entries);
        }

        public async Task<Result<PlayerFollowEntry>> FindByIdPairIncludingFanAsync(string fanId, Guid playerId)
        {
            var entry = await context.PlayerFollowEntries
                .Include(x => x.Fan)
                .FirstOrDefaultAsync(x => x.FanId == fanId && x.PlayerId == playerId);

            return entry == null
                ? Result<PlayerFollowEntry>.Failure("Player follow entry not found.")
                : Result<PlayerFollowEntry>.Success(entry);
        }
    }
}
