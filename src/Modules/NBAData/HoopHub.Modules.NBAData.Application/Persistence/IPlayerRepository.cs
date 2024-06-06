using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.Players;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface IPlayerRepository : IAsyncRepository<Player>
    {
        Task<Result<IReadOnlyList<Player>>> GetAllActivePlayersByTeam(Guid teamId);
        Task<Result<IReadOnlyList<Player>>> GetAllActivePlayersByTeamApiId(int teamId);
        Task<Result<Player>> FindByApiIdAsync(int apiId);
        Task<Result<IReadOnlyList<Player>>> GetAllActivePlayers();
    }
}
