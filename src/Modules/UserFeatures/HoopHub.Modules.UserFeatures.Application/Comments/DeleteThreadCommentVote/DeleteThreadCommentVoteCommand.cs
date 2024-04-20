using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.DeleteThreadCommentVote
{
    public class DeleteThreadCommentVoteCommand : IRequest<BaseResponse>
    {
        public Guid CommentId { get; set; }
    }
}
