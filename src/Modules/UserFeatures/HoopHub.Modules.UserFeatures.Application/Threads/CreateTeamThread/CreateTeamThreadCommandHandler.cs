using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateTeamThread
{
    public class CreateTeamThreadCommandHandler(ICurrentUserService currentUserService, ITeamThreadRepository teamThreadRepository) : IRequestHandler<CreateTeamThreadCommand, Response<TeamThreadDto>>
    {
        private readonly ICurrentUserService _userService = currentUserService;
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly TeamThreadMapper _teamThreadMapper = new();

        public async Task<Response<TeamThreadDto>> Handle(CreateTeamThreadCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTeamThreadCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<TeamThreadDto>.ErrorResponseFromFluentResult(validationResult);


            var fanId = _userService.GetUserId;
            if (string.IsNullOrEmpty(fanId))
                return Response<TeamThreadDto>.ErrorResponseFromKeyMessage(ValidationErrors.InvalidFanId, ValidationKeys.FanId);

            var teamThreadResult = TeamThread.Create(fanId, request.TeamId, request.Title, request.Content);
            if (!teamThreadResult.IsSuccess)
                return Response<TeamThreadDto>.ErrorResponseFromKeyMessage(teamThreadResult.ErrorMsg, ValidationKeys.TeamThread);

            var teamThread = teamThreadResult.Value;
            var createTeamThreadResult = await _teamThreadRepository.AddAsync(teamThread);
            if (!createTeamThreadResult.IsSuccess)
                return Response<TeamThreadDto>.ErrorResponseFromKeyMessage(createTeamThreadResult.ErrorMsg, ValidationKeys.TeamThread);

            var returnTeamThread = await _teamThreadRepository.FindByIdAsyncIncludingFan(teamThread.Id);
            return new Response<TeamThreadDto>
            {
                Data = _teamThreadMapper.TeamThreadToTeamThreadDto(returnTeamThread.Value),
                Success = true
            };
        }
    }
}
