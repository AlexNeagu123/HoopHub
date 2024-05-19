using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Fans;

namespace HoopHub.Modules.UserFeatures.Application.Fans.Mappers
{
    public class FanMapper
    {
        public FanDto FanToFanDto(Fan fan)
        {
            return new FanDto
            {
                Id = fan.Id,
                Username = fan.Username,
                UpVotes = fan.UpVotes,
                DownVotes = fan.DownVotes,
                FanBadge = fan.FanBadge,
                FavouriteTeamId = fan.FavouriteTeamId,
                AvatarPhotoUrl = fan.AvatarPhotoUrl,
                CommentsCount = fan.CommentsCount,
                ReviewsCount = fan.ReviewsCount,
            };
        }
    }
}
