using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.UpdateThreadCommentVote
{
    public class UpdateThreadCommentVoteCommand : IRequest<Response<ThreadCommentVoteDto>>
    {
        public Guid CommentId { get; set; }
        public bool IsUpvote { get; set; }
    }
}
