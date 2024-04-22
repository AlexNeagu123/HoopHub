using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Fans;

namespace HoopHub.Modules.UserFeatures.Application.Fans.Mappers
{
    public class FanMapper
    {
        public FanDto FanToFanDto(Fan fan)
        {
            Console.WriteLine(fan);
            Console.WriteLine(fan.Id);
            Console.WriteLine(fan.Username);
            Console.WriteLine(fan.UpVotes);
            Console.WriteLine(fan.DownVotes);
            Console.WriteLine(fan.FanBadge);
            Console.WriteLine(fan.FavouriteTeamId);
            Console.WriteLine(fan.AvatarPhotoUrl);

            return new FanDto
            {
                Id = fan.Id,
                Username = fan.Username,
                UpVotes = fan.UpVotes,
                DownVotes = fan.DownVotes,
                FanBadge = fan.FanBadge,
                FavouriteTeamId = fan.FavouriteTeamId,
                AvatarPhotoUrl = fan.AvatarPhotoUrl
            };
        }
    }
}
