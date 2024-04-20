using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.UpdateThreadCommentVote
{
    public class UpdateThreadCommentVoteCommandHandler(
        IThreadCommentVoteRepository threadCommentVoteRepository,
        ICurrentUserService userService)
        : IRequestHandler<UpdateThreadCommentVoteCommand, Response<ThreadCommentVoteDto>>
    {
        private readonly IThreadCommentVoteRepository _threadCommentVoteRepository = threadCommentVoteRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly ThreadCommentVoteMapper _threadCommentVoteMapper = new();
        public async Task<Response<ThreadCommentVoteDto>> Handle(UpdateThreadCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new UpdateThreadCommentVoteCommandValidator(_threadCommentVoteRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<ThreadCommentVoteDto>.ErrorResponseFromFluentResult(validationResult);

            var threadCommentVoteResult = await _threadCommentVoteRepository.FindByIdAsyncIncludingAll(request.CommentId, fanId!);
            if (!threadCommentVoteResult.IsSuccess)
                return Response<ThreadCommentVoteDto>.ErrorResponseFromKeyMessage(threadCommentVoteResult.ErrorMsg, ValidationKeys.ThreadCommentVote);

            var threadCommentVote = threadCommentVoteResult.Value;
            threadCommentVote.Update(request.IsUpvote);

            var updateThreadCommentVoteResult = await _threadCommentVoteRepository.UpdateAsync(threadCommentVote);
            if (!updateThreadCommentVoteResult.IsSuccess)
                return Response<ThreadCommentVoteDto>.ErrorResponseFromKeyMessage(updateThreadCommentVoteResult.ErrorMsg, ValidationKeys.ThreadCommentVote);

            return new Response<ThreadCommentVoteDto>
            {
                Success = true,
                Data = _threadCommentVoteMapper.CommentVoteToCommentVoteDto(threadCommentVote)
            };
        }
    }
}
