using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface ITeamRepository : IAsyncRepository<Team>
    {
    }
}
