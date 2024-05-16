using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadReplyComment
{
    public class CreateThreadReplyCommentCommandHandler(IThreadCommentRepository threadCommentRepository, ICurrentUserService currentUserService, ITeamThreadRepository teamThreadRepository, IFanRepository fanRepository, IGameThreadRepository gameThreadRepository) : IRequestHandler<CreateThreadReplyCommentCommand, Response<ThreadCommentDto>>
    {
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly IGameThreadRepository _gameThreadRepository = gameThreadRepository;
        private readonly IFanRepository _fanRepository = fanRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ThreadCommentMapper _threadCommentMapper = new();
        public async Task<Response<ThreadCommentDto>> Handle(CreateThreadReplyCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateThreadReplyCommentCommandValidator(_threadCommentRepository, _teamThreadRepository, _gameThreadRepository, _fanRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<ThreadCommentDto>.ErrorResponseFromFluentResult(validationResult);

            var fanId = _currentUserService.GetUserId;
            var fanResult = await _fanRepository.FindByIdAsync(fanId!);

            var threadCommentResult = ThreadComment.Create(request.Content, fanId!);
            if (!threadCommentResult.IsSuccess)
                return Response<ThreadCommentDto>.ErrorResponseFromKeyMessage(threadCommentResult.ErrorMsg, ValidationKeys.ThreadComment);

            var threadComment = threadCommentResult.Value;
            threadComment.AttachParentId(request.ParentCommentId);
            threadComment.AttachRespondsToFanId(request.RespondsToFanId);

            var parentCommentResult = await _threadCommentRepository.FindByIdAsyncIncludingAll(request.ParentCommentId);
            var parentComment = parentCommentResult.Value;

            if (request.TeamThreadId.HasValue)
            {
                threadComment.AttachTeamThread(request.TeamThreadId);
                var teamThread = await _teamThreadRepository.FindByIdAsyncIncludingFan(request.TeamThreadId.Value);
                threadComment.NotifyThreadOwner(teamThread.Value.FanId, fanResult.Value, parentComment.Id, teamThread.Value);
                threadComment.NotifyCommentOwner(parentComment.FanId, fanResult.Value, parentComment.Id, teamThread.Value, null);
            }

            if (request.GameThreadId.HasValue)
            {
                threadComment.AttachGameThread(request.GameThreadId);
                var gameThread = await _gameThreadRepository.FindByIdAsync(request.GameThreadId.Value);
                threadComment.NotifyCommentOwner(parentComment.FanId, fanResult.Value, parentComment.Id, null, gameThread.Value);
            }


            threadComment.MarkAsAdded();

            var addThreadCommentResult = await _threadCommentRepository.AddAsync(threadComment);
            if (!addThreadCommentResult.IsSuccess)
                return Response<ThreadCommentDto>.ErrorResponseFromKeyMessage(addThreadCommentResult.ErrorMsg, ValidationKeys.ThreadComment);

            var addedThreadComment = await _threadCommentRepository.FindByIdAsyncIncludingAll(addThreadCommentResult.Value.Id);
            return new Response<ThreadCommentDto>
            {
                Success = true,
                Data = _threadCommentMapper.ThreadCommentToThreadCommentDto(addedThreadComment.Value)
            };
        }
    }
}
