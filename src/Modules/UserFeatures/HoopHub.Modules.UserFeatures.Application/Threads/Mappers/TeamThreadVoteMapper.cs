using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Application.Threads.Mappers
{
    public class TeamThreadVoteMapper
    {
        private readonly FanMapper _fanMapper = new();
        private readonly TeamThreadMapper _teamThreadMapper = new();

        public TeamThreadVoteDto TeamThreadVoteToTeamThreadVoteDto(TeamThreadVote teamThreadVote)
        {
            return new TeamThreadVoteDto
            {
                Fan = _fanMapper.FanToFanDto(teamThreadVote.Fan),
                TeamThread = _teamThreadMapper.TeamThreadToTeamThreadDto(teamThreadVote.TeamThread),
                IsUpvote = teamThreadVote.IsUpVote
            };
        }
    }
}
