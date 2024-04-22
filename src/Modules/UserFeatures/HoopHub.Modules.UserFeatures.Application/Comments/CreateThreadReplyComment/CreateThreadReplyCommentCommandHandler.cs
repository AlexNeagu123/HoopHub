using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments;
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
            var validator = new CreateThreadReplyCommentCommandValidator(_threadCommentRepository, _teamThreadRepository, _gameThreadRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<ThreadCommentDto>.ErrorResponseFromFluentResult(validationResult);

            var fanId = _currentUserService.GetUserId;
            var threadCommentResult = ThreadComment.Create(request.Content, fanId!);
            if (!threadCommentResult.IsSuccess)
                return Response<ThreadCommentDto>.ErrorResponseFromKeyMessage(threadCommentResult.ErrorMsg, ValidationKeys.ThreadComment);

            var threadComment = threadCommentResult.Value;
            threadComment.AttachParentId(request.ParentCommentId);

            if (request.TeamThreadId != null)
            {
                threadComment.AttachTeamThread(request.TeamThreadId);
                var teamThread = await _teamThreadRepository.FindByIdAsyncIncludingFan(request.TeamThreadId.Value);
                var fan = await _fanRepository.FindByIdAsync(fanId!);
                threadComment.NotifyThreadOwner(teamThread.Value.FanId, fan.Value);
            }

            if (request.GameThreadId != null)
                threadComment.AttachGameThread(request.GameThreadId);

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
