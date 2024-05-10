using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetCommentsPagedByUserId
{
    public class GetCommentsPagedByUserIdQuery : IRequest<PagedResponse<IReadOnlyList<ThreadCommentDto>>>
    {
        public string UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool IsPopular { get; set; } = false;
    }
}
