using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.TeamsLatest;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface ITeamLatestRepository : IAsyncRepository<TeamLatest>
    {
        Task<Result<IReadOnlyList<TeamLatest>>> GetLatestByTeam(Guid teamId);
    }
}
