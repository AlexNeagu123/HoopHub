using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetAllTeams
{
    public class GetAllTeamsQueryHandler(ITeamRepository teamRepository)
        : IRequestHandler<GetAllTeamsQuery, Response<IReadOnlyList<TeamDto>>>
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly TeamMapper _teamMapper = new();

        public async Task<Response<IReadOnlyList<TeamDto>>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _teamRepository.FindAllActive();
            var teams = queryResult.Value;
            var teamDtoList = teams.Select(t => _teamMapper.TeamToTeamDto(t)).ToList();
            return new Response<IReadOnlyList<TeamDto>>
            {
                Success = true,
                Data = teamDtoList
            };
        }
    }
}
