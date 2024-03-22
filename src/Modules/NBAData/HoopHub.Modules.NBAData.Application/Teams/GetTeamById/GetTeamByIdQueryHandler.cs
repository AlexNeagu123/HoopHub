using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetTeamById
{
    public class GetTeamByIdQueryHandler(ITeamRepository teamRepository, IPlayerRepository playerRepository)
        : IRequestHandler<GetTeamByIdQuery, Response<TeamDto>>
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly TeamMapper _teamMapper = new();

        public async Task<Response<TeamDto>> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTeamByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new Response<TeamDto>
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Take(1).ToDictionary(error => error.PropertyName, error => error.ErrorMessage)
                };
            }
            
            var queryResult = await _teamRepository.FindByIdAsyncIncludingPlayers(request.TeamId);
            if (queryResult.IsSuccess == false)
            {
                return new Response<TeamDto>
                {
                    Success = false,
                    ValidationErrors = new Dictionary<string, string> { { "PlayerId", queryResult.ErrorMsg } }
                };
            }

            var team = queryResult.Value;
            var teamDto = _teamMapper.TeamToTeamDto(team);
            return new Response<TeamDto>
            {
                Success = true,
                Data = teamDto
            };
        }
    }
}
