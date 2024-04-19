using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.UpdateTeamThread
{
    public class UpdateTeamThreadCommandHandler(ITeamThreadRepository teamThreadRepository, ICurrentUserService currentUserService) : IRequestHandler<UpdateTeamThreadCommand, Response<TeamThreadDto>>
    {
        private readonly ITeamThreadRepository _threadRepository = teamThreadRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly TeamThreadMapper _teamThreadMapper = new();
        public async Task<Response<TeamThreadDto>> Handle(UpdateTeamThreadCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTeamThreadCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<TeamThreadDto>.ErrorResponseFromFluentResult(validationResult);

            var threadResult = await _threadRepository.FindByIdAsyncIncludingFan(request.Id);
            if (!threadResult.IsSuccess)
                return Response<TeamThreadDto>.ErrorResponseFromKeyMessage(threadResult.ErrorMsg, ValidationKeys.TeamThread);

            var thread = threadResult.Value;
            var userId = _currentUserService.GetUserId;

            if (thread.FanId != userId)
                return Response<TeamThreadDto>.ErrorResponseFromKeyMessage(ValidationErrors.ThreadUpdateNotAuthorized, ValidationKeys.TeamThread);

            thread.Update(request.Title, request.Content);
            var updateResult = await _threadRepository.UpdateAsync(thread);
            if (!updateResult.IsSuccess)
                return Response<TeamThreadDto>.ErrorResponseFromKeyMessage(updateResult.ErrorMsg, ValidationKeys.TeamThread);

            return new Response<TeamThreadDto>
            {
                Success = true,
                Data = _teamThreadMapper.TeamThreadToTeamThreadDto(thread)
            };
        }
    }
}
