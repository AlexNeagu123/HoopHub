using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Dtos
{
    public class TeamFollowEntryDto
    {
        public FanDto Fan { get; set; } = null!;
        public Guid TeamId { get; set; }
    }
}
