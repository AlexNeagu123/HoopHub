using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.DeletePlayerFollowEntry
{
    public class DeletePlayerFollowEntryCommand : IRequest<BaseResponse>
    {
        public Guid PlayerId { get; set; }
    }
}
