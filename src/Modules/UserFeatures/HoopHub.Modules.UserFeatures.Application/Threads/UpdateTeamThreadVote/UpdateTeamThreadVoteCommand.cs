using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.UpdateTeamThreadVote
{
    public class UpdateTeamThreadVoteCommand : IRequest<Response<TeamThreadVoteDto>>
    {
        public Guid ThreadId { get; set; }
        public bool IsUpvote { get; set; }
    }
}
