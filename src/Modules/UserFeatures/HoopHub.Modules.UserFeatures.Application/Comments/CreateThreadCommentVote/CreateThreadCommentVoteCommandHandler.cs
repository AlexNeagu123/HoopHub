using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadCommentVote
{
    public class CreateThreadCommentVoteCommandHandler(IThreadCommentRepository threadCommentRepository, ICurrentUserService userService,
        IThreadCommentVoteRepository threadCommentVoteRepository) : IRequestHandler<CreateThreadCommentVoteCommand, Response<ThreadCommentVoteDto>>
    {
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly IThreadCommentVoteRepository _threadCommentVoteRepository = threadCommentVoteRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly ThreadCommentVoteMapper _threadCommentVoteMapper = new();
        public async Task<Response<ThreadCommentVoteDto>> Handle(CreateThreadCommentVoteCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new CreateThreadCommentVoteCommandValidator(_threadCommentRepository, _threadCommentVoteRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<ThreadCommentVoteDto>.ErrorResponseFromFluentResult(validationResult);

            var threadCommentVoteResult = CommentVote.Create(request.CommentId, fanId!, request.IsUpvote);
            if (!threadCommentVoteResult.IsSuccess)
                return Response<ThreadCommentVoteDto>.ErrorResponseFromKeyMessage(threadCommentVoteResult.ErrorMsg, ValidationKeys.ThreadCommentVote);

            var threadCommentVote = threadCommentVoteResult.Value;
            threadCommentVote.MarkAsAdded();

            var addThreadCommentVoteResult = await _threadCommentVoteRepository.AddAsync(threadCommentVote);
            if (!addThreadCommentVoteResult.IsSuccess)
                return Response<ThreadCommentVoteDto>.ErrorResponseFromKeyMessage(addThreadCommentVoteResult.ErrorMsg, ValidationKeys.ThreadCommentVote);

            var addedThreadCommentVote = await _threadCommentVoteRepository.FindByIdAsyncIncludingAll(threadCommentVote.CommentId, threadCommentVote.FanId);
            return new Response<ThreadCommentVoteDto>
            {
                Success = true,
                Data = _threadCommentVoteMapper.CommentVoteToCommentVoteDto(addedThreadCommentVote.Value)
            };
        }
    }
}
