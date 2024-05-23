using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.BoxScores;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface IBoxScoresRepository : IAsyncRepository<BoxScores>
    {
        Task<Result<IReadOnlyList<BoxScores>>> GetLastXBoxScoresByPlayerId(Guid playerId, int lastCount);
        Task<Result<IReadOnlyList<BoxScores>>> GetByDateAsync(DateTime date);
        Task<Result<BoxScores>> FindByIdIncludingAll(Guid id);
    }
}
