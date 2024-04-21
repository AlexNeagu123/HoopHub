using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.CreateTeamFollowEntry
{
    public class CreateTeamFollowEntryCommand : IRequest<Response<TeamFollowEntryDto>>
    {
        public Guid TeamId { get; set; }
    }
}
