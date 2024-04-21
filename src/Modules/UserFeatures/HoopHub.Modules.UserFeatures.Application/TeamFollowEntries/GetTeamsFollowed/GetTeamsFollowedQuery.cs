using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.GetTeamsFollowed
{
    public class GetTeamsFollowedQuery : IRequest<Response<IReadOnlyList<TeamFollowEntryDto>>>
    {
    }
}
