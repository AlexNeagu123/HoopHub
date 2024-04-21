using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadComment
{
    public class CreateThreadCommentCommandHandler(ICurrentUserService userService, IThreadCommentRepository threadCommentRepository, ITeamThreadRepository teamThreadRepository, IGameThreadRepository gameThreadRepository) : IRequestHandler<CreateThreadCommentCommand, Response<ThreadCommentDto>>
    {
        private readonly ICurrentUserService _currentUserService = userService;
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly IGameThreadRepository _gameThreadRepository = gameThreadRepository;
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly ThreadCommentMapper _threadCommentMapper = new();
        public async Task<Response<ThreadCommentDto>> Handle(CreateThreadCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateThreadCommentCommandValidator(_teamThreadRepository, _gameThreadRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<ThreadCommentDto>.ErrorResponseFromFluentResult(validationResult);

            var fanId = _currentUserService.GetUserId;
            var threadCommentResult = ThreadComment.Create(request.Content, fanId!);
            if (!threadCommentResult.IsSuccess)
                return Response<ThreadCommentDto>.ErrorResponseFromKeyMessage(threadCommentResult.ErrorMsg, ValidationKeys.ThreadComment);

            var threadComment = threadCommentResult.Value;
            if (request.TeamThreadId != null)
                threadComment.AttachTeamThread(request.TeamThreadId);

            if (request.GameThreadId != null)
                threadComment.AttachGameThread(request.GameThreadId);

            var addResult = await _threadCommentRepository.AddAsync(threadComment);
            if (!addResult.IsSuccess)
                return Response<ThreadCommentDto>.ErrorResponseFromKeyMessage(addResult.ErrorMsg, ValidationKeys.ThreadComment);

            var completeThreadComment = await _threadCommentRepository.FindByIdAsyncIncludingAll(threadComment.Id);
            return new Response<ThreadCommentDto>
            {
                Success = true,
                Data = _threadCommentMapper.ThreadCommentToThreadCommentDto(completeThreadComment.Value)
            };
        }
    }
}
