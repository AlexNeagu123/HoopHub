using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class PlayerTeamSeasonRepository(NBADataContext context)
        : BaseRepository<PlayerTeamSeason>(context), IPlayerTeamSeasonRepository
    {
        public async Task<Result<IReadOnlyList<PlayerTeamSeason>>> GetTeamHistoryByPlayerId(Guid playerId)
        {
            var teamHistory = await Context.Set<PlayerTeamSeason>()
                .Include(p => p.Team)
                .Include(p => p.Season)
                .Where(p => p.PlayerId == playerId)
                .ToListAsync();

            return Result<IReadOnlyList<PlayerTeamSeason>>.Success(teamHistory);
        }
    }
}
