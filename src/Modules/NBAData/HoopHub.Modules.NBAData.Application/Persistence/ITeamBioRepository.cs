using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.TeamBios;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface ITeamBioRepository : IAsyncRepository<TeamBio>
    {

    }
}
