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
    public class CreateThreadCommentCommandHandler(ICurrentUserService userService, IThreadCommentRepository threadCommentRepository, ITeamThreadRepository teamThreadRepository, IGameThreadRepository gameThreadRepository, IFanRepository fanRepository) : IRequestHandler<CreateThreadCommentCommand, Response<ThreadCommentDto>>
    {
        private readonly ICurrentUserService _currentUserService = userService;
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly IGameThreadRepository _gameThreadRepository = gameThreadRepository;
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly IFanRepository _fanRepository = fanRepository;
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
            if (request.TeamThreadId.HasValue)
            {
                threadComment.AttachTeamThread(request.TeamThreadId);
                var teamThread = await _teamThreadRepository.FindByIdAsyncIncludingFan(request.TeamThreadId.Value);
                var fan = await _fanRepository.FindByIdAsync(fanId!);
                threadComment.NotifyThreadOwner(teamThread.Value.FanId, fan.Value);
            }

            if (request.GameThreadId.HasValue)
                threadComment.AttachGameThread(request.GameThreadId);

            threadComment.MarkAsAdded();
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
