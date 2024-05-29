using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.CreateBoxScore
{
    public class CreateBoxScoreCommandHandler(
        IBoxScoresRepository boxScoresRepository,
        ITeamRepository teamRepository,
        IPlayerRepository playerRepository,
        IGameRepository gameRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<CreateBoxScoreCommand, Response<LocalStoredBoxScoresDto>>
    {
        private readonly IBoxScoresRepository _boxScoresRepository = boxScoresRepository;
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly LocalBoxScoresMapper _localBoxScoresMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<LocalStoredBoxScoresDto>> Handle(CreateBoxScoreCommand request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var teamResult = await _teamRepository.FindByApiIdAsync(request.TeamId);
            if (!teamResult.IsSuccess)
                return Response<LocalStoredBoxScoresDto>.ErrorResponseFromKeyMessage(teamResult.ErrorMsg, ValidationKeys.TeamId);

            var playerResult = await _playerRepository.FindByApiIdAsync(request.PlayerId);
            if (!playerResult.IsSuccess)
                return Response<LocalStoredBoxScoresDto>.ErrorResponseFromKeyMessage(playerResult.ErrorMsg, ValidationKeys.PlayerId);


            var homeTeamResult = await _teamRepository.FindByApiIdAsync(request.HomeTeamId);
            if (!homeTeamResult.IsSuccess)
                return Response<LocalStoredBoxScoresDto>.ErrorResponseFromKeyMessage(homeTeamResult.ErrorMsg, ValidationKeys.TeamId);

            var visitorTeamResult = await _teamRepository.FindByApiIdAsync(request.VisitorTeamId);
            if (!visitorTeamResult.IsSuccess)
                return Response<LocalStoredBoxScoresDto>.ErrorResponseFromKeyMessage(visitorTeamResult.ErrorMsg, ValidationKeys.TeamId);


            var gameResult = await _gameRepository.FindGameByDateAndTeams(request.Date, homeTeamResult.Value.Id, visitorTeamResult.Value.Id);
            if (!gameResult.IsSuccess)
                return Response<LocalStoredBoxScoresDto>.ErrorResponseFromKeyMessage(gameResult.ErrorMsg, ValidationKeys.Game);

            var boxScore = Domain.BoxScores.BoxScores.Create(
                gameResult.Value.Id,
                playerResult.Value.Id,
                teamResult.Value.Id,
                    request.Min,
                    request.Fgm,
                    request.Fga,
                    request.FgPct,
                    request.Fg3m,
                    request.Fg3a,
                    request.Fg3Pct,
                    request.Ftm,
                    request.Fta,
                    request.FtPct,
                    request.Oreb,
                    request.Dreb,
                    request.Reb,
                    request.Ast,
                    request.Stl,
                    request.Blk,
                    request.Turnover,
                    request.Pf,
                    request.Pts
            );

            boxScore.MarkAsAdded(homeTeamResult.Value.ApiId, visitorTeamResult.Value.ApiId, $"{playerResult.Value.FirstName} {playerResult.Value.LastName}", 
                gameResult.Value.Date, playerResult.Value.ImageUrl);

            var addResult = await _boxScoresRepository.AddAsync(boxScore);
            if (!addResult.IsSuccess)
                return Response<LocalStoredBoxScoresDto>.ErrorResponseFromKeyMessage(addResult.ErrorMsg, ValidationKeys.BoxScores);


            var afterAddResult = await _boxScoresRepository.FindByIdIncludingAll(addResult.Value.Id);
            if (!afterAddResult.IsSuccess)
                return Response<LocalStoredBoxScoresDto>.ErrorResponseFromKeyMessage(afterAddResult.ErrorMsg, ValidationKeys.BoxScores);

            return new Response<LocalStoredBoxScoresDto>
            {
                Success = true,
                Data = _localBoxScoresMapper.LocalStoredBoxScoresToLocalStoredBoxScoresDto(afterAddResult.Value, isLicensed)
            };
        }
    }
}
