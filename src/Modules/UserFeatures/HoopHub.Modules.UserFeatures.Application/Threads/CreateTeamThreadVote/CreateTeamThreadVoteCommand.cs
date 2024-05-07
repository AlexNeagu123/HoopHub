using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateTeamThreadVote
{
    public class CreateTeamThreadVoteCommand : IRequest<Response<TeamThreadVote>>
    {
        public Guid ThreadId { get; set; }
        public bool IsUpvote { get; set; }
    }
}
