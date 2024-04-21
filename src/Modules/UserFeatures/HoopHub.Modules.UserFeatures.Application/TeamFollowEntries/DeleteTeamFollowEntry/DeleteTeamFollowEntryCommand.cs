using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.DeleteTeamFollowEntry
{
    public class DeleteTeamFollowEntryCommand : IRequest<BaseResponse>
    {
        public Guid TeamId { get; set; }
    }
}
