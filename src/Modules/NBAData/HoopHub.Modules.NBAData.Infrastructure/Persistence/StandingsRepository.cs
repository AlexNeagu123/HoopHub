using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Standings;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class StandingsRepository(NBADataContext context) : BaseRepository<StandingsEntry>(context), IStandingsRepository
    {
        public async Task<Result<IReadOnlyList<StandingsEntry>>> GetStandingsBySeasonPeriod(string seasonPeriod)
        {
            var standings = await Context.Set<StandingsEntry>()
                .Include(s => s.Team)
                .Include(s => s.Season)
                .Where(s => s.Season.SeasonPeriod == seasonPeriod)
                .OrderBy(s => s.Rank)
                .ToListAsync();

            return Result<IReadOnlyList<StandingsEntry>>.Success(standings);
        }
    }
}
