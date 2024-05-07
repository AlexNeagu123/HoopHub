using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.DeleteTeamThreadVote
{
    public class DeleteTeamThreadVoteCommand : IRequest<BaseResponse>
    {
        public Guid ThreadId { get; set; }
    }
}
