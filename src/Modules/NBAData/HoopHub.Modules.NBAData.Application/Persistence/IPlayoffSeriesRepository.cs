using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.Standings;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface IPlayoffSeriesRepository : IAsyncRepository<PlayoffSeries>
    {
        public Task<Result<IReadOnlyList<PlayoffSeries>>> GetPlayoffSeriesBySeasonAsync(string seasonPeriod);
    }
}
