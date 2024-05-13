using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetRepliesByComment
{
    public class GetRepliesByCommentQuery : IRequest<Response<IReadOnlyList<ThreadCommentDto>>>
    {
        public Guid CommentId { get; set; }
    }
}
