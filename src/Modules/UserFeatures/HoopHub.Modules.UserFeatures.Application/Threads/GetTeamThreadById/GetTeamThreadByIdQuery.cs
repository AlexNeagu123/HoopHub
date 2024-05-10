using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadById
{
    public class GetTeamThreadByIdQuery : IRequest<Response<TeamThreadDto>>
    {
        public Guid TeamThreadId { get; set; }
    }
}
