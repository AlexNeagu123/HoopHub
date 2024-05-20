using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.SeasonAverageStats;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetBioByPlayerId
{
    public class GetBioByPlayerIdQueryHandler(IPlayerTeamSeasonRepository playerTeamSeasonRepository,
        ISeasonAverageStatsService averageStatsService) : IRequestHandler<GetBioByPlayerIdQuery, Response<PlayerDto>>
    {
        private readonly IPlayerTeamSeasonRepository _playerTeamSeasonRepository = playerTeamSeasonRepository;
        private readonly ISeasonAverageStatsService _averageStatsService = averageStatsService;
        private readonly TeamMapper _teamMapper = new();
        private readonly Mappers.PlayerMapper _playerMapper = new();

        public async Task<Response<PlayerDto>> Handle(GetBioByPlayerIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBioByPlayerIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<PlayerDto>.ErrorResponseFromFluentResult(validationResult);

            var queryResult = await _playerTeamSeasonRepository.GetTeamHistoryByPlayerId(request.PlayerId);
            if (!queryResult.IsSuccess)
                return Response<PlayerDto>.ErrorResponseFromKeyMessage(queryResult.ErrorMsg, ValidationKeys.PlayerId);

            var playerHistory = queryResult.Value;
            if (playerHistory.Count == 0)
                return Response<PlayerDto>.ErrorResponseFromKeyMessage(ErrorMessages.PlayerBioNotFound, ValidationKeys.PlayerBio);

            var playerDto = _playerMapper.PlayerToPlayerDto(playerHistory[0].Player);
            foreach (var seasonInfo in playerHistory)
            {
                var intSeason = GetIntSeasonStart(seasonInfo.Season.SeasonPeriod);
                if (intSeason < request.StartSeason || intSeason >= request.EndSeason)
                    continue;

                var statsResult = await _averageStatsService.GetAverageStatsBySeasonIdAndPlayerIdAsync(seasonInfo.Season.SeasonPeriod, playerDto.ApiId);
                if (!statsResult.IsSuccess)
                    return Response<PlayerDto>.ErrorResponseFromKeyMessage(statsResult.ErrorMsg, ValidationKeys.AverageStats);

                var stats = statsResult.Value;
                seasonInfo.Team.Players = null!;
                stats.Team = _teamMapper.TeamToTeamDto(seasonInfo.Team);
                playerDto.SeasonAverageStats.Add(stats);
            }

            return new Response<PlayerDto>
            {
                Success = true,
                Data = playerDto
            };
        }

        private static int GetIntSeasonStart(string seasonPeriod)
        {
            var parts = seasonPeriod.Split('-');
            return parts.Length != 2 ? 0 : int.Parse(parts[0]);
        }
    }
}
