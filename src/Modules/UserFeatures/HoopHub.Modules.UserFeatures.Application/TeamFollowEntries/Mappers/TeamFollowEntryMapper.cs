using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Follows;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Mappers
{
    public class TeamFollowEntryMapper
    {
        private readonly FanMapper _fanMapper = new();
        public TeamFollowEntryDto TeamFollowEntryToTeamFollowEntryDto(TeamFollowEntry teamFollowEntry)
        {
            return new TeamFollowEntryDto
            {
                Fan = _fanMapper.FanToFanDto(teamFollowEntry.Fan),
                TeamId = teamFollowEntry.TeamId,
            };
        }
    }
}
