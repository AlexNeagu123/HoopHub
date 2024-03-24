using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
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

                var homeTeam = await _teamRepository.FindByApiIdAsync(boxScore.HomeTeam.Id);
                if (homeTeam.IsSuccess == false)
                    return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(homeTeam.ErrorMsg, ValidationKeys.TeamApiId);

                var visitorTeam = await _teamRepository.FindByApiIdAsync(boxScore.VisitorTeam.Id);
                if (visitorTeam.IsSuccess == false)
                    return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(visitorTeam.ErrorMsg, ValidationKeys.TeamApiId);

                var boxScoreHomeTeam = _boxScoreTeamMapper.TeamToBoxScoreTeamDto(homeTeam.Value);
                var boxScoreVisitorTeam = _boxScoreTeamMapper.TeamToBoxScoreTeamDto(visitorTeam.Value);
                
                foreach(var apiPlayer in boxScore.HomeTeam.Players)
                {
                    var boxScorePlayerDto = _boxScorePlayerMapper.BoxScoreApiPlayerDtoToBoxScorePlayerDto(apiPlayer);
                    var player = await _playerRepository.FindByApiIdAsync(apiPlayer.Player.Id);
                    if (player.IsSuccess == false)
                        continue;

                    boxScorePlayerDto.Player = _playerMapper.PlayerToPlayerDto(player.Value);
                    boxScoreHomeTeam.Players.Add(boxScorePlayerDto);
                }

                foreach (var apiPlayer in boxScore.VisitorTeam.Players)
                {
                    var boxScorePlayerDto = _boxScorePlayerMapper.BoxScoreApiPlayerDtoToBoxScorePlayerDto(apiPlayer);
                    var player = await _playerRepository.FindByApiIdAsync(apiPlayer.Player.Id);
                    if (player.IsSuccess == false)
                        continue;

                    boxScorePlayerDto.Player = _playerMapper.PlayerToPlayerDto(player.Value);
                    boxScoreVisitorTeam.Players.Add(boxScorePlayerDto);
                }

                var gameWithBoxScore = _gameWithBoxScoreMapper.BoxScoreApiDtoToGameWithBoxScoreDto(boxScore);
                gameWithBoxScore.HomeTeam = boxScoreHomeTeam;
                gameWithBoxScore.VisitorTeam = boxScoreVisitorTeam;

                return new Response<GameWithBoxScoreDto>
                {
                    Success = true,
                    Data = gameWithBoxScore
                };
            }

            return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(ErrorMessages.BoxScoreNotFound, ValidationKeys.BoxScores);
        }
    }
}
