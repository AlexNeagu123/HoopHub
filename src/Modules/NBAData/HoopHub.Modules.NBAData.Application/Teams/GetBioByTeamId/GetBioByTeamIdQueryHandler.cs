using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetBioByTeamId
{
    public class GetBioByTeamIdQueryHandler(ITeamRepository teamRepository)
        : IRequestHandler<GetBioByTeamIdQuery, Response<TeamDto>>
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly TeamMapper _teamMapper = new();

        public async Task<Response<TeamDto>> Handle(GetBioByTeamIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBioByTeamIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<TeamDto>.ErrorResponseFromFluentResult(validationResult);

            var queryResult = await _teamRepository.FindByIdAsyncIncludingBio(request.TeamId);
            if (queryResult.IsSuccess == false)
                return Response<TeamDto>.ErrorResponseFromKeyMessage(queryResult.ErrorMsg, ValidationKeys.TeamId);

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
