using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Dtos;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.GetTeamsFollowed
{
    public class GetTeamsFollowedQueryHandler(
        ITeamFollowEntryRepository teamFollowEntryRepository,
        ICurrentUserService userService)
        : IRequestHandler<GetTeamsFollowedQuery, Response<IReadOnlyList<TeamFollowEntryDto>>>
    {
        private readonly ITeamFollowEntryRepository _teamFollowEntryRepository = teamFollowEntryRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly TeamFollowEntryMapper _teamFollowEntryMapper = new();

        public async Task<Response<IReadOnlyList<TeamFollowEntryDto>>> Handle(GetTeamsFollowedQuery request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var teamFollowEntriesResult = await _teamFollowEntryRepository.GetAllByFanIdIncludingFanAsync(fanId!);
            if (!teamFollowEntriesResult.IsSuccess)
                return Response<IReadOnlyList<TeamFollowEntryDto>>.ErrorResponseFromKeyMessage(teamFollowEntriesResult.ErrorMsg, ValidationKeys.TeamFollowEntry);

            var teamFollowEntries = teamFollowEntriesResult.Value;
            var teamFollowEntryDtoList = teamFollowEntries.Select(_teamFollowEntryMapper.TeamFollowEntryToTeamFollowEntryDto).ToList();

            return new Response<IReadOnlyList<TeamFollowEntryDto>>
            {
                Success = true,
                Data = teamFollowEntryDtoList
            };
        }
    }
}
