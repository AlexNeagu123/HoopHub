using HoopHub.Modules.NBAData.Domain.Teams;
using Riok.Mapperly.Abstractions;

namespace HoopHub.Modules.NBAData.Application.Teams
{
    [Mapper]
    public partial class TeamMapper
    {
        public partial TeamDto TeamToTeamDto(Team team);
    }
}
