using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.DeleteThreadCommentVote
{
    public class DeleteThreadCommentVoteCommandHandler(
        IThreadCommentVoteRepository threadCommentVoteRepository,
        ICurrentUserService userService)
        : IRequestHandler<DeleteThreadCommentVoteCommand, BaseResponse>
    {
        private readonly IThreadCommentVoteRepository _threadCommentVoteRepository = threadCommentVoteRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly ThreadCommentVoteMapper _threadCommentVoteMapper = new();

        public async Task<BaseResponse> Handle(DeleteThreadCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new DeleteThreadCommentVoteCommandValidator(_threadCommentVoteRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BaseResponse.ErrorResponseFromFluentResult(validationResult);

            var threadCommentVoteResult = await _threadCommentVoteRepository.FindByIdAsyncIncludingAll(request.CommentId, fanId!);
            if (!threadCommentVoteResult.IsSuccess)
                return BaseResponse.ErrorResponseFromKeyMessage(threadCommentVoteResult.ErrorMsg, ValidationKeys.ThreadCommentVote);

            var threadCommentVote = threadCommentVoteResult.Value;
            threadCommentVote.MarkAsDeleted();
            var deleteThreadCommentVoteResult = await _threadCommentVoteRepository.RemoveAsync(threadCommentVote);

            return deleteThreadCommentVoteResult.IsSuccess
                ? new BaseResponse { Success = true }
                : BaseResponse.ErrorResponseFromKeyMessage(deleteThreadCommentVoteResult.ErrorMsg, ValidationKeys.ThreadCommentVote);
        }
    }
}
