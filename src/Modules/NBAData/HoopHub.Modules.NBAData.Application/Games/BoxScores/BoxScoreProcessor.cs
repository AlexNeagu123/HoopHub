using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Players;
using PlayerMapper = HoopHub.Modules.NBAData.Application.Players.Mappers.PlayerMapper;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores
{
    public class BoxScoreProcessor(ITeamRepository teamRepository, IPlayerRepository playerRepository)
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly BoxScoreTeamMapper _boxScoreTeamMapper = new();
        private readonly BoxScorePlayerMapper _boxScorePlayerMapper = new();
        private readonly PlayerMapper _playerMapper = new();
        private readonly GameWithBoxScoreMapper _gameWithBoxScoreMapper = new();

        public async Task<Response<GameWithBoxScoreDto>> ProcessApiBoxScoreAndConvert(BoxScoreApiDto boxScore)
        {
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
                if(apiPlayer.Player == null) 
                    continue;

                var boxScorePlayerDto = _boxScorePlayerMapper.BoxScoreApiPlayerDtoToBoxScorePlayerDto(apiPlayer);
                var player = await _playerRepository.FindByApiIdAsync(apiPlayer.Player.Id);
                if (player.IsSuccess == false)
                    continue;

                var playerDto = _playerMapper.PlayerToPlayerDto(player.Value);
                boxScorePlayerDto.Player = playerDto;
                boxScoreHomeTeam.Players.Add(boxScorePlayerDto);
            }

            foreach (var apiPlayer in boxScore.VisitorTeam.Players)
            {
                if (apiPlayer.Player == null)
                    continue;

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
    }
}
