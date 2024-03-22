﻿using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.SeasonAverageStats;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Teams;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetBioByPlayerId
{
    public class GetBioByPlayerIdQueryHandler(IPlayerTeamSeasonRepository playerTeamSeasonRepository,
        ISeasonAverageStatsService averageStatsService) : IRequestHandler<GetBioByPlayerIdQuery, Response<PlayerDto>>
    {
        private readonly IPlayerTeamSeasonRepository _playerTeamSeasonRepository = playerTeamSeasonRepository;
        private readonly ISeasonAverageStatsService _averageStatsService = averageStatsService;
        private readonly TeamMapper _teamMapper = new();
        private readonly PlayerMapper _playerMapper = new();

        public async Task<Response<PlayerDto>> Handle(GetBioByPlayerIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBioByPlayerIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new Response<PlayerDto>
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Take(1).ToDictionary(error => error.PropertyName, error => error.ErrorMessage)
                };
            }

            var queryResult = await _playerTeamSeasonRepository.GetTeamHistoryByPlayerId(request.PlayerId);
            if (!queryResult.IsSuccess)
            {
                return new Response<PlayerDto>
                {
                    Success = false,
                    ValidationErrors = new Dictionary<string, string> { { "PlayerId", queryResult.ErrorMsg } }
                };
            }

            var playerHistory = queryResult.Value;
            if (playerHistory.Count == 0)
            {
                return new Response<PlayerDto>
                {
                    Success = false,
                    ValidationErrors = new Dictionary<string, string> { { "PlayerBio", "Player bio not found" } }
                };
            }

            var playerDto = _playerMapper.PlayerToPlayerDto(playerHistory[0].Player);
            foreach (var seasonInfo in playerHistory)
            {
                var statsResult = await _averageStatsService.GetAverageStatsBySeasonIdAndPlayerIdAsync(seasonInfo.Season.SeasonPeriod, playerDto.ApiId);
                if (!statsResult.IsSuccess)
                {
                    return new Response<PlayerDto>
                    {
                        Success = false,
                        ValidationErrors = new Dictionary<string, string> { { "AverageStats", statsResult.ErrorMsg } }
                    };
                }

                var stats = statsResult.Value;
                stats.Team = _teamMapper.TeamToTeamDto(seasonInfo.Team);
                playerDto.SeasonAverageStats.Add(stats);
            }

            return new Response<PlayerDto>
            {
                Success = true,
                Data = playerDto
            };
        }
    }
}
