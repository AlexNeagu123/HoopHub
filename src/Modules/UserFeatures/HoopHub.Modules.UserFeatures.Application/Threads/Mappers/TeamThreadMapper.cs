using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Application.Threads.Mappers
{
    public class TeamThreadMapper
    {
        private readonly FanMapper _fanMapper = new();
        public TeamThreadDto TeamThreadToTeamThreadDto(TeamThread teamThread)
        {
            return new TeamThreadDto
            {
                Id = teamThread.Id,
                TeamId = teamThread.TeamId,
                Fan = _fanMapper.FanToFanDto(teamThread.Fan),
                Title = teamThread.Title,
                Content = teamThread.Content,
                CreatedDate = teamThread.CreatedDate,
                UpVotes = teamThread.UpVotes,
                DownVotes = teamThread.DownVotes
            };
        }
    }
}
