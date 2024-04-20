using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.DeleteThreadComment
{
    public class DeleteThreadCommentCommandHandler(IThreadCommentRepository threadCommentRepository, ICurrentUserService userService) : IRequestHandler<DeleteThreadCommentCommand, BaseResponse>
    {
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly ICurrentUserService _userService = userService;

        public async Task<BaseResponse> Handle(DeleteThreadCommentCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new DeleteThreadCommentCommandValidator(_threadCommentRepository, fanId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BaseResponse.ErrorResponseFromFluentResult(validationResult);

            var deleteThreadCommentResult = await _threadCommentRepository.DeleteAsync(request.CommentId);
            if (!deleteThreadCommentResult.IsSuccess)
                return BaseResponse.ErrorResponseFromKeyMessage(deleteThreadCommentResult.ErrorMsg, ValidationKeys.ThreadComment);

            return new BaseResponse
            {
                Success = true
            };
        }
    }
}
