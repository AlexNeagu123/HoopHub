using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Follows;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Mappers
{
    public class PlayerFollowEntryMapper
    {
        private readonly FanMapper _fanMapper = new();
        public PlayerFollowEntryDto PlayerFollowEntryToPlayerFollowEntryDto(PlayerFollowEntry playerFollowEntry)
        {
            return new PlayerFollowEntryDto
            {
                Fan = _fanMapper.FanToFanDto(playerFollowEntry.Fan),
                PlayerId = playerFollowEntry.PlayerId,
            };
        }
    }
}
