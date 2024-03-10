using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Infrastructure.Persistence
{
    public class TeamRepository(NBADataContext context) : BaseRepository<Team>(context), ITeamRepository
    {
    }
}
