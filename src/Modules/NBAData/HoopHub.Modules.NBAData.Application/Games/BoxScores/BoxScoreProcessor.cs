using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using PlayerMapper = HoopHub.Modules.NBAData.Application.Players.Mappers.PlayerMapper;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores
{
    public class BoxScoreProcessor(ITeamRepository teamRepository, IPlayerRepository playerRepository, bool isLicensed)
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly BoxScoreTeamMapper _boxScoreTeamMapper = new();
        private readonly BoxScorePlayerMapper _boxScorePlayerMapper = new();
        private readonly PlayerMapper _playerMapper = new();
        private readonly GameWithBoxScoreMapper _gameWithBoxScoreMapper = new();
        private readonly bool _isLicensed = isLicensed;

        public async Task<Response<GameWithBoxScoreDto>> ProcessApiBoxScoreAndConvert(BoxScoreApiDto boxScore)
        {
            var homeTeam = await _teamRepository.FindByApiIdAsync(boxScore.HomeTeam.Id);
            if (homeTeam.IsSuccess == false)
                return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(homeTeam.ErrorMsg, ValidationKeys.TeamApiId);

            var visitorTeam = await _teamRepository.FindByApiIdAsync(boxScore.VisitorTeam.Id);
            if (visitorTeam.IsSuccess == false)
                return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(visitorTeam.ErrorMsg, ValidationKeys.TeamApiId);

            var boxScoreHomeTeam = _boxScoreTeamMapper.TeamToBoxScoreTeamDto(homeTeam.Value, _isLicensed);
            var boxScoreVisitorTeam = _boxScoreTeamMapper.TeamToBoxScoreTeamDto(visitorTeam.Value, _isLicensed);

            foreach (var apiPlayer in boxScore.HomeTeam.Players)
            {
                if (apiPlayer.Player == null)
                    continue;

                var boxScorePlayerDto = _boxScorePlayerMapper.BoxScoreApiPlayerDtoToBoxScorePlayerDto(apiPlayer);
                boxScorePlayerDto.PlayerApiId = apiPlayer.Player.Id;
                boxScoreHomeTeam.Players.Add(boxScorePlayerDto);
            }

            foreach (var apiPlayer in boxScore.VisitorTeam.Players)
            {
                if (apiPlayer.Player == null)
                    continue;

                var boxScorePlayerDto = _boxScorePlayerMapper.BoxScoreApiPlayerDtoToBoxScorePlayerDto(apiPlayer);
                boxScorePlayerDto.PlayerApiId = apiPlayer.Player.Id;
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
