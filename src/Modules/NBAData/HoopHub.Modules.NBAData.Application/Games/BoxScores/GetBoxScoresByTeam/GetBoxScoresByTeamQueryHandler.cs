using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByTeam
{
    public class GetBoxScoresByTeamQueryHandler(IBoxScoresDataService boxScoresDataService, ITeamRepository teamRepository, IPlayerRepository playerRepository, ITeamLatestRepository teamLatestRepository, IGameRepository gameRepository, ICurrentUserService currentUserService) : IRequestHandler<GetBoxScoresByTeamQuery, Response<IReadOnlyList<LocalStoredGameDto>>>
    {
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly LocalGameMapper _localGameMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;
        public async Task<Response<IReadOnlyList<LocalStoredGameDto>>> Handle(GetBoxScoresByTeamQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetBoxScoresByTeamQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<LocalStoredGameDto>>.ErrorResponseFromFluentResult(validationResult);

            var gamesResult = await _gameRepository.GetLastXGamesByTeam(request.TeamId, request.GameCount);
            if (!gamesResult.IsSuccess)
                return Response<IReadOnlyList<LocalStoredGameDto>>.ErrorResponseFromKeyMessage(gamesResult.ErrorMsg, ValidationKeys.Games);

            var gamesResultDto = gamesResult.Value.Select(game => _localGameMapper.LocalStoredGameToLocalStoredGameDto(game, isLicensed)).ToList();

            return new Response<IReadOnlyList<LocalStoredGameDto>>
            {
                Success = true,
                Data = gamesResultDto
            };
        }
    }
}
