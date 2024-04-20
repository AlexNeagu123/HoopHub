using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetCommentsPagedByThread
{
    public class GetCommentsPagedByThreadQuery : IRequest<PagedResponse<IReadOnlyList<ThreadCommentDto>>>
    {
        public Guid? TeamThreadId { get; set; }
        public Guid? GameThreadId { get; set; }
        public Guid? FirstComment { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool IsPopular { get; set; } = false;
    }
}
