﻿using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Games;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class GameRepository(NBADataContext context) : BaseRepository<Game>(context), IGameRepository
    {
        public async Task<Result<IReadOnlyList<Game>>> GetLastXGamesByTeam(Guid teamId, int lastCount)
        {
            var games = await context.Set<Game>()
                .Include(g => g.HomeTeam)
                .Include(g => g.VisitorTeam)
                .Include(g => g.Season)
                .Where(g => g.HomeTeamId == teamId || g.VisitorTeamId == teamId)
                .Where(g => g.Status == "Final")
                .OrderByDescending(g => g.Date)
                .Take(lastCount)
                .ToListAsync();
            return Result<IReadOnlyList<Game>>.Success(games);
        }

        public async Task<Result<IReadOnlyList<Game>>> FindGamesByDate(DateTime date)
        {
            var games = await context.Set<Game>()
                .Include(g => g.HomeTeam)
                .Include(g => g.VisitorTeam)
                .Include(g => g.Season)
                .Where(g => g.Date == date)
                .ToListAsync();

            return Result<IReadOnlyList<Game>>.Success(games);
        }

        public async Task<Result<Game>> FindGameByDateAndTeams(DateTime date, Guid homeTeamId, Guid visitorTeamId)
        {
            var game = await context.Set<Game>()
                .Include(g => g.HomeTeam)
                .Include(g => g.VisitorTeam)
                .Include(g => g.Season)
                .Where(g => g.Date == date)
                .Where(g => g.HomeTeamId == homeTeamId
                            && g.VisitorTeamId == visitorTeamId)
                .FirstOrDefaultAsync();

            return game == null ? Result<Game>.Failure($"Entity with Date {date} and Teams {homeTeamId} and {visitorTeamId} not found")
                : Result<Game>.Success(game);
        }

        public async Task<Result<IReadOnlyList<Game>>> GetGamesForTeamSinceStartOfSeason(DateTime seasonStart, DateTime gameDate, int teamApiId)
        {
            var games = await context.Set<Game>()
                .Include(g => g.BoxScores)
                .Include(g => g.Season)
                .Include(g => g.HomeTeam)
                .Include(g => g.VisitorTeam)
                .Where(g => (g.HomeTeam.ApiId == teamApiId || g.VisitorTeam.ApiId == teamApiId) && g.Date >= seasonStart && g.Date < gameDate)
                .OrderByDescending(g => g.Date)
                .ToListAsync();

            return Result<IReadOnlyList<Game>>.Success(games);
        }

        public async Task<Result<Game>> FindByIdIncludingAll(Guid id)
        {
            var game = await context.Set<Game>()
                .Include(g => g.HomeTeam)
                .Include(g => g.VisitorTeam)
                .Include(g => g.Season)
                .FirstOrDefaultAsync(g => g.Id == id);
            return game == null ? Result<Game>.Failure($"Entity with Id {id} not found") : Result<Game>.Success(game);
        }
    }
}
