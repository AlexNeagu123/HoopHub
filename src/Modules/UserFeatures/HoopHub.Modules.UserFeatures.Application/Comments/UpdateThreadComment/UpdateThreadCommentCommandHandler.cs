using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.UpdateThreadComment
{
    public class UpdateThreadCommentCommandHandler(IThreadCommentRepository threadCommentRepository, ICurrentUserService userService) : IRequestHandler<UpdateThreadCommentCommand, Response<ThreadCommentDto>>
    {
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly ThreadCommentMapper _threadCommentMapper = new();
        public async Task<Response<ThreadCommentDto>> Handle(UpdateThreadCommentCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new UpdateThreadCommentCommandValidator(_threadCommentRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<ThreadCommentDto>.ErrorResponseFromFluentResult(validationResult);

            var threadCommentResult = await _threadCommentRepository.FindByIdAsyncIncludingAll(request.CommentId);
            if (!threadCommentResult.IsSuccess)
                return Response<ThreadCommentDto>.ErrorResponseFromKeyMessage(threadCommentResult.ErrorMsg, ValidationKeys.ThreadComment);

            var threadComment = threadCommentResult.Value;
            threadComment.Update(request.Content);

            var updateThreadCommentResult = await _threadCommentRepository.UpdateAsync(threadComment);
            if (!updateThreadCommentResult.IsSuccess)
                return Response<ThreadCommentDto>.ErrorResponseFromKeyMessage(updateThreadCommentResult.ErrorMsg, ValidationKeys.ThreadComment);

            return new Response<ThreadCommentDto>
            {
                Success = true,
                Data = _threadCommentMapper.ThreadCommentToThreadCommentDto(threadComment)
            };
        }
    }
}
