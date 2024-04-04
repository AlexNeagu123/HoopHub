using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.BoxScoreas;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Players;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.GetBoxScoreByGame
{
    public class GetBoxScoreByGameQueryHandler(IBoxScoresDataService boxScoresDataService, ITeamRepository teamRepository, IPlayerRepository playerRepository) 
        : IRequestHandler<GetBoxScoreByGameQuery, Response<GameWithBoxScoreDto>>
    {
        private readonly IBoxScoresDataService _boxScoresDataService = boxScoresDataService;
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly BoxScoreTeamMapper _boxScoreTeamMapper = new();
        private readonly BoxScorePlayerMapper _boxScorePlayerMapper = new();
        private readonly PlayerMapper _playerMapper = new();
        private readonly GameWithBoxScoreMapper _gameWithBoxScoreMapper = new();

        public async Task<Response<GameWithBoxScoreDto>> Handle(GetBoxScoreByGameQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBoxScoreByGameQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<GameWithBoxScoreDto>.ErrorResponseFromFluentResult(validationResult);

            var apiBoxScoresResult = await _boxScoresDataService.GetBoxScoresAsyncByDate(request.Date);
            if (apiBoxScoresResult.IsSuccess == false)
                return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(apiBoxScoresResult.ErrorMsg, ValidationKeys.BoxScores);

            var apiBoxScores = apiBoxScoresResult.Value;
            foreach (var boxScore in apiBoxScores)
            {
                if (boxScore.HomeTeam == null || boxScore.VisitorTeam == null) continue;
                
                if (boxScore.HomeTeam.Id != request.HomeTeamApiId || boxScore.VisitorTeam.Id != request.VisitorTeamApiId)
                    continue;

                BoxScoreProcessor boxScoreProcessor = new(_boxScoresDataService, _teamRepository, _playerRepository);
                return await boxScoreProcessor.ProcessApiBoxScoreAndConvert(boxScore);
            }

            return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(ErrorMessages.BoxScoreNotFound, ValidationKeys.BoxScores);
        }
    }
}
