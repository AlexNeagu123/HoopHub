using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.TeamsLatest;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class TeamLatestRepository(NBADataContext context) : BaseRepository<TeamLatest>(context), ITeamLatestRepository
    {
        public async Task<Result<IReadOnlyList<TeamLatest>>> GetLatestByTeam(Guid teamId)
        {
            var latestByTeam =  await Context.Set<TeamLatest>()
                .Include(t => t.Team)
                .Where(tl => tl.TeamId == teamId).ToListAsync();
            return Result<IReadOnlyList<TeamLatest>>.Success(latestByTeam);
        }
    }
}
