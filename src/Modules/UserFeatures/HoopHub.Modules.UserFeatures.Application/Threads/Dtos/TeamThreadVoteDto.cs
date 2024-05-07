using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.Threads.Dtos
{
    public class TeamThreadVoteDto
    {
        public FanDto Fan { get; set; }
        public TeamThreadDto TeamThread { get; set; }
        public bool IsUpvote { get; set; }
    }
}
