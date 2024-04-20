using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.DeleteThreadComment
{
    public class DeleteThreadCommentCommand : IRequest<BaseResponse>
    {
        public Guid CommentId { get; set; }
    }
}
