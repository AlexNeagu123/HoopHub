using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.TeamBios;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class TeamBioRepository(NBADataContext context) : BaseRepository<TeamBio>(context), ITeamBioRepository
    {
    }
}
