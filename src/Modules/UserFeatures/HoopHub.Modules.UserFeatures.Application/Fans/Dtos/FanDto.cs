using HoopHub.Modules.UserFeatures.Domain.Fans;

namespace HoopHub.Modules.UserFeatures.Application.Fans.Dtos
{
    public class FanDto
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public int CommentsCount { get; set; }
        public int ReviewsCount { get; set; }
        public FanBadgeType FanBadge { get; set; }
        public Guid? FavouriteTeamId { get; set; }
        public string? AvatarPhotoUrl { get; set; }
    }
}
