using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Teams;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class TeamRepository(NBADataContext context) : BaseRepository<Team>(context), ITeamRepository
    {

        public virtual async Task<Result<IReadOnlyList<Team>>> FindAllActive()
        {
            var result = await Context.Set<Team>().Where(t => t.IsActive).ToListAsync();
            return Result<IReadOnlyList<Team>>.Success(result);
        }

        public virtual async Task<Result<Team>> FindByIdAsyncIncludingPlayers(Guid id)
        {
            var result = await Context.Set<Team>().Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);
            return result == null ? Result<Team>.Failure($"Entity with PlayerId {id} not found") : Result<Team>.Success(result);
        }

        public virtual async Task<Result<Team>> FindByIdAsyncIncludingBio(Guid id)
        {
            var result = await Context.Set<Team>().Include(t => t.TeamBio)
                .ThenInclude(tb => tb.Season)
                .FirstOrDefaultAsync(t => t.Id == id);
            return result == null ? Result<Team>.Failure($"Entity with PlayerId {id} not found") : Result<Team>.Success(result);
        }

        public virtual async Task<Result<Team>> FindByApiIdAsync(int apiId)
        {
            var result = await Context.Set<Team>().FirstOrDefaultAsync(t => t.ApiId == apiId);
            return result == null ? Result<Team>.Failure($"Entity with ApiId {apiId} not found") : Result<Team>.Success(result);
        }
    }
}
