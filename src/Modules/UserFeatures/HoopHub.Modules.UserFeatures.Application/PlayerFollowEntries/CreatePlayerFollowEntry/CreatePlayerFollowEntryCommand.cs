using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.CreatePlayerFollowEntry
{
    public class CreatePlayerFollowEntryCommand : IRequest<Response<PlayerFollowEntryDto>>
    {
        public Guid PlayerId { get; set; }
    }
}
