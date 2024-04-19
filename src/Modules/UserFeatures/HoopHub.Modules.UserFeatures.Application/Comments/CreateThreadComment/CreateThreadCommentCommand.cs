using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadComment
{
    public class CreateThreadCommentCommand : IRequest<Response<ThreadCommentDto>>
    {
        public string Content { get; set; }
        public Guid? TeamThreadId { get; set; }
        public Guid? GameThreadId { get; set; }
    }
}
