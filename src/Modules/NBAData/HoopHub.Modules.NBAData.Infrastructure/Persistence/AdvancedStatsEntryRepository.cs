using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.AdvancedStatsEntries;
using HoopHub.Modules.NBAData.Domain.BoxScores;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class AdvancedStatsEntryRepository(NBADataContext context) : BaseRepository<AdvancedStatsEntry>(context), IAdvancedStatsEntryRepository
    {
        public async Task<Result<IReadOnlyList<AdvancedStatsEntry>>> GetLastXAdvancedStatsByPlayerId(Guid playerId, int lastCount)
        {
            var advancedStatsEntries = await context.Set<AdvancedStatsEntry>()
                .Include(ase => ase.Game)
                .Include(ase => ase.Player)
                .Include(ase => ase.Team)
                .Include(ase => ase.Game.Season)
                .Include(ase => ase.Game.HomeTeam)
                .Include(ase => ase.Game.VisitorTeam)
                .Where(ase => ase.PlayerId == playerId)
                .Where(ase => ase.Game.Status == "Final")
                .OrderByDescending(ase => ase.Game.Date)
                .Take(lastCount)
                .ToListAsync();

            return Result<IReadOnlyList<AdvancedStatsEntry>>.Success(advancedStatsEntries);
        }

        public async Task<Result<IReadOnlyList<AdvancedStatsEntry>>> GetByDateAsync(DateTime date)
        {
            var advancedStatsEntries = await context.Set<AdvancedStatsEntry>()
                .Include(ase => ase.Game)
                .Include(ase => ase.Player)
                .Include(ase => ase.Team)
                .Include(ase => ase.Game.Season)
                .Include(ase => ase.Game.HomeTeam)
                .Include(ase => ase.Game.VisitorTeam)
                .Where(ase => ase.Game.Date == date)
                .ToListAsync();

            return Result<IReadOnlyList<AdvancedStatsEntry>>.Success(advancedStatsEntries);
        }

        public async Task<Result<AdvancedStatsEntry>> FindByIdIncludingAll(Guid id)
        {
            var advancedStatsEntry = await context.Set<AdvancedStatsEntry>()
                .Include(ase => ase.Game)
                .Include(ase => ase.Player)
                .Include(ase => ase.Team)
                .Include(ase => ase.Game.Season)
                .Include(ase => ase.Game.HomeTeam)
                .Include(ase => ase.Game.VisitorTeam)
                .FirstOrDefaultAsync(ase => ase.Id == id);

            return advancedStatsEntry == null
                ? Result<AdvancedStatsEntry>.Failure($"Entity with Id {id} not found")
                : Result<AdvancedStatsEntry>.Success(advancedStatsEntry);
        }

        public async Task<Result<IReadOnlyList<AdvancedStatsEntry>>> GetByGameAsync(Guid gameId)
        {
            var advancedStatsEntries = await context.Set<AdvancedStatsEntry>()
                .Include(ase => ase.Game)
                .Include(ase => ase.Player)
                .Include(ase => ase.Team)
                .Include(ase => ase.Game.Season)
                .Include(ase => ase.Game.HomeTeam)
                .Include(ase => ase.Game.VisitorTeam)
                .Where(ase => ase.GameId == gameId)
                .ToListAsync();

            return Result<IReadOnlyList<AdvancedStatsEntry>>.Success(advancedStatsEntries);
        }

        public async Task<Result<IReadOnlyList<AdvancedStatsEntry>>> GetBoxScoresForTeamSinceStartOfSeason(DateTime seasonStartDate, DateTime gameDate, int teamApiId)
        {
            var advancedStats = await context.Set<AdvancedStatsEntry>()
                .Include(bs => bs.Game)
                .Include(bs => bs.Player)
                .Include(bs => bs.Team)
                .Include(bs => bs.Game.Season)
                .Include(bs => bs.Game.HomeTeam)
                .Include(bs => bs.Game.VisitorTeam)
                .Where(bs => bs.Team.ApiId == teamApiId && bs.Game.Date >= seasonStartDate && bs.Game.Date < gameDate)
                .OrderByDescending(bs => bs.Game.Date)
                .ToListAsync();

            return Result<IReadOnlyList<AdvancedStatsEntry>>.Success(advancedStats);
        }
    }
}
