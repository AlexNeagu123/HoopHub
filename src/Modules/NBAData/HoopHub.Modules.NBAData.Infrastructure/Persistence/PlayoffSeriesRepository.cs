using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Standings;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class PlayoffSeriesRepository(NBADataContext dbContext) : BaseRepository<PlayoffSeries>(dbContext), IPlayoffSeriesRepository
    {
        public async Task<Result<IReadOnlyList<PlayoffSeries>>> GetPlayoffSeriesBySeasonAsync(string seasonPeriod)
        {
            var standings = await Context.Set<PlayoffSeries>()
                .Include(s => s.WinningTeam)
                .Include(s => s.LosingTeam)
                .Include(s => s.Season)
                .Where(s => s.Season.SeasonPeriod == seasonPeriod)
                .ToListAsync();

            return Result<IReadOnlyList<PlayoffSeries>>.Success(standings);
        }
    }
}
