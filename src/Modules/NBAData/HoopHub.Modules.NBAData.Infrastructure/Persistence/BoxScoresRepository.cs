﻿using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.BoxScores;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class BoxScoresRepository(NBADataContext context) : BaseRepository<BoxScores>(context), IBoxScoresRepository
    {
        public async Task<Result<IReadOnlyList<BoxScores>>> GetLastXBoxScoresByPlayerId(Guid playerId, int lastCount)
        {
            var boxScores = await context.Set<BoxScores>()
                .Include(bs => bs.Game)
                .Include(bs => bs.Player)
                .Include(bs => bs.Team)
                .Include(bs => bs.Game.Season)
                .Include(bs => bs.Game.HomeTeam)
                .Include(bs => bs.Game.VisitorTeam)
                .Where(bs => bs.PlayerId == playerId)
                .Where(bs => bs.Game.Status == "Final")
                .OrderByDescending(bs => bs.Game.Date)
                .Take(lastCount)
                .ToListAsync();

            return Result<IReadOnlyList<BoxScores>>.Success(boxScores);
        }
    }
}