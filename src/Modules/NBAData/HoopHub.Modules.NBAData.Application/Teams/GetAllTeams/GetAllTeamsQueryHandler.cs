using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetAllTeams
{
    public class GetAllTeamsQueryHandler(ITeamRepository teamRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetAllTeamsQuery, Response<IReadOnlyList<TeamDto>>>
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly TeamMapper _teamMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<IReadOnlyList<TeamDto>>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _teamRepository.FindAllActive();
            var teams = queryResult.Value;
            var isLicensed = _currentUserService.GetUserLicense ?? false;

            var teamDtoList = teams.Select(t => _teamMapper.TeamToTeamDto(t, isLicensed)).ToList();
            return new Response<IReadOnlyList<TeamDto>>
            {
                Success = true,
                Data = teamDtoList
            };
        }
    }
}
