using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface IPlayerTeamSeasonRepository : IAsyncRepository<PlayerTeamSeason>
    {
        Task<Result<IReadOnlyList<PlayerTeamSeason>>> GetTeamHistoryByPlayerId(Guid playerId);
    }
}
