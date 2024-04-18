using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;

namespace HoopHub.Modules.UserFeatures.Domain.Threads
{
    public class TeamThread : BaseThread
    {
        public Guid TeamId { get; private set; }
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public string Title { get; private set; }
        public string Content { get; private set; }
        private TeamThread(string fanId, Guid teamId, string title, string content)
        {
            FanId = fanId;
            TeamId = teamId;
            Title = title;
            Content = content;
        }

        public static Result<TeamThread> Create(string fanId, Guid teamId, string title, string content)
        {
            return Result<TeamThread>.Success(new TeamThread(fanId, teamId, title, content));
        }
    }
}
