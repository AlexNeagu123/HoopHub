using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Dtos;
using MediatR;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Follows;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.CreateTeamFollowEntry
{
    public class CreateTeamFollowEntryCommandHandler(
        ITeamFollowEntryRepository teamFollowEntryRepository,
        ICurrentUserService userService)
        : IRequestHandler<CreateTeamFollowEntryCommand, Response<TeamFollowEntryDto>>
    {
        private readonly ITeamFollowEntryRepository _teamFollowEntryRepository = teamFollowEntryRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly TeamFollowEntryMapper _teamFollowEntryMapper = new();

        public async Task<Response<TeamFollowEntryDto>> Handle(CreateTeamFollowEntryCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new CreateTeamFollowEntryCommandValidator(_teamFollowEntryRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<TeamFollowEntryDto>.ErrorResponseFromFluentResult(validationResult);

            var teamFollowEntryResult = TeamFollowEntry.Create(fanId!, request.TeamId);
            if (!teamFollowEntryResult.IsSuccess)
                return Response<TeamFollowEntryDto>.ErrorResponseFromKeyMessage(teamFollowEntryResult.ErrorMsg, ValidationKeys.TeamFollowEntry);

            var teamFollowEntry = teamFollowEntryResult.Value;
            var createTeamFollowEntryResult = await _teamFollowEntryRepository.AddAsync(teamFollowEntry);
            if (!createTeamFollowEntryResult.IsSuccess)
                return Response<TeamFollowEntryDto>.ErrorResponseFromKeyMessage(createTeamFollowEntryResult.ErrorMsg, ValidationKeys.TeamFollowEntry);

            var completedEntryResult = await _teamFollowEntryRepository.FindByIdPairIncludingFanAsync(fanId!, request.TeamId);
            return new Response<TeamFollowEntryDto>
            {
                Data = _teamFollowEntryMapper.TeamFollowEntryToTeamFollowEntryDto(completedEntryResult.Value),
                Success = true
            };
        }
    }
}
