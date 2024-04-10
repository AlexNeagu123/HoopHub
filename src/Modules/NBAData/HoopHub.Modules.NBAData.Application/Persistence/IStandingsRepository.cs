using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.Standings;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface IStandingsRepository : IAsyncRepository<StandingsEntry>
    {
        Task<Result<IReadOnlyList<StandingsEntry>>> GetStandingsBySeasonPeriod(string seasonPeriod);
    }
}
