using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Seasons;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class SeasonRepository(NBADataContext context) : BaseRepository<Season>(context), ISeasonRepository
    {
        public async Task<Result<Season>> FindBySeasonPeriod(string seasonPeriod)
        {
            var season = await context.Set<Season>()
                .FirstOrDefaultAsync(s => s.SeasonPeriod == seasonPeriod);
            return season == null ? Result<Season>.Failure($"Entity with SeasonPeriod {seasonPeriod} not found") : Result<Season>.Success(season);
        }
    }
}
