using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Dtos
{
    public class PlayerFollowEntryDto
    {
        public FanDto Fan { get; set; } = null!;
        public Guid PlayerId { get; set; }
    }
}
