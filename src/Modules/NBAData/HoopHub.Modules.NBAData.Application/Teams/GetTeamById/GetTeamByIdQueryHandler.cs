using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.TeamBios.Mappers;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetTeamById
{
    public class GetTeamByIdQueryHandler(ITeamRepository teamRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetTeamByIdQuery, Response<TeamDto>>
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly TeamBioMapper _teamBioMapper = new();
        private readonly TeamMapper _teamMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<TeamDto>> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetTeamByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<TeamDto>.ErrorResponseFromFluentResult(validationResult);

            var queryResult = await _teamRepository.FindByIdAsyncIncludingPlayers(request.TeamId);
            if (queryResult.IsSuccess == false)
                return Response<TeamDto>.ErrorResponseFromKeyMessage(queryResult.ErrorMsg, ValidationKeys.TeamId);

            var team = queryResult.Value;
            var teamDto = _teamMapper.TeamToTeamDto(team, isLicensed);

            var bioQueryResult = await _teamRepository.FindByIdAsyncIncludingBio(request.TeamId);
            if (bioQueryResult.IsSuccess == false)
                return Response<TeamDto>.ErrorResponseFromKeyMessage(bioQueryResult.ErrorMsg, ValidationKeys.TeamId);

            foreach (var teamBio in bioQueryResult.Value.TeamBio)
            {
                if (teamBio.Season.SeasonPeriod != Config.CurrentSeasonStr)
                    continue;

                teamDto.TeamBio.Add(_teamBioMapper.TeamBioToTeamBioDto(teamBio));
                break;
            }

            return new Response<TeamDto>
            {
                Success = true,
                Data = teamDto
            };
        }
    }
}
