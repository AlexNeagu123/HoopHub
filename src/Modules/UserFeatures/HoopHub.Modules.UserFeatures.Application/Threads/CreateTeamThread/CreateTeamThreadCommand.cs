using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateTeamThread
{
    public class CreateTeamThreadCommand : IRequest<Response<TeamThreadDto>>
    {
        public Guid TeamId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
