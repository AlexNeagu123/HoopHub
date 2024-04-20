using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.UpdateThreadComment
{
    public class UpdateThreadCommentCommand : IRequest<Response<ThreadCommentDto>>
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
