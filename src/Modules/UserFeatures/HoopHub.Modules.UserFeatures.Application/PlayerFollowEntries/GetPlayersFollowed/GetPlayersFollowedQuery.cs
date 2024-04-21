using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.GetPlayersFollowed
{
    public class GetPlayersFollowedQuery : IRequest<Response<IReadOnlyList<PlayerFollowEntryDto>>>
    {
    }
}
