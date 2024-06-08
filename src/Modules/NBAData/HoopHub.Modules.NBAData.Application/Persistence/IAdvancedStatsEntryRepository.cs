using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface IAdvancedStatsEntryRepository : IAsyncRepository<Domain.AdvancedStatsEntries.AdvancedStatsEntry>
    {
        Task<Result<IReadOnlyList<Domain.AdvancedStatsEntries.AdvancedStatsEntry>>> GetLastXAdvancedStatsByPlayerId(Guid playerId, int lastCount);
        Task<Result<IReadOnlyList<Domain.AdvancedStatsEntries.AdvancedStatsEntry>>> GetByDateAsync(DateTime date);
        Task<Result<Domain.AdvancedStatsEntries.AdvancedStatsEntry>> FindByIdIncludingAll(Guid id);
        Task<Result<IReadOnlyList<Domain.AdvancedStatsEntries.AdvancedStatsEntry>>> GetAdvancedStatsByGameAsync(DateTime gameDate, int homeTeamApiId, int visitorTeamApiId);
        Task<Result<IReadOnlyList<Domain.AdvancedStatsEntries.AdvancedStatsEntry>>> GetBoxScoresForTeamSinceStartOfSeason(DateTime seasonStartDate, DateTime gameDate, int teamApiId);
    }
}
