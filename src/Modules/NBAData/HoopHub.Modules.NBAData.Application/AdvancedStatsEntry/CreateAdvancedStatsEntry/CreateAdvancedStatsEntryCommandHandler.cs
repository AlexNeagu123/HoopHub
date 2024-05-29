using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Dtos;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.CreateAdvancedStatsEntry
{
    public class CreateAdvancedStatsEntryCommandHandler(
        IAdvancedStatsEntryRepository advancedStatsEntryRepository,
        ITeamRepository teamRepository,
        IPlayerRepository playerRepository,
        IGameRepository gameRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<CreateAdvancedStatsEntryCommand, Response<LocalStoredAdvancedStatsEntryDto>>
    {
        private readonly IAdvancedStatsEntryRepository _advancedStatsEntryRepository = advancedStatsEntryRepository;
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly LocalAdvancedStatsMapper _localAdvancedStatsMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<LocalStoredAdvancedStatsEntryDto>> Handle(CreateAdvancedStatsEntryCommand request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var teamResult = await _teamRepository.FindByApiIdAsync(request.TeamId);
            if (!teamResult.IsSuccess)
                return Response<LocalStoredAdvancedStatsEntryDto>.ErrorResponseFromKeyMessage(teamResult.ErrorMsg, ValidationKeys.TeamId);

            var playerResult = await _playerRepository.FindByApiIdAsync(request.PlayerId);
            if (!playerResult.IsSuccess)
                return Response<LocalStoredAdvancedStatsEntryDto>.ErrorResponseFromKeyMessage(playerResult.ErrorMsg, ValidationKeys.PlayerId);

            var homeTeamResult = await _teamRepository.FindByApiIdAsync(request.HomeTeamId);
            if (!homeTeamResult.IsSuccess)
                return Response<LocalStoredAdvancedStatsEntryDto>.ErrorResponseFromKeyMessage(homeTeamResult.ErrorMsg, ValidationKeys.TeamId);

            var visitorTeamResult = await _teamRepository.FindByApiIdAsync(request.VisitorTeamId);
            if (!visitorTeamResult.IsSuccess)
                return Response<LocalStoredAdvancedStatsEntryDto>.ErrorResponseFromKeyMessage(visitorTeamResult.ErrorMsg, ValidationKeys.TeamId);


            var gameResult = await _gameRepository.FindGameByDateAndTeams(request.Date, homeTeamResult.Value.Id, visitorTeamResult.Value.Id);
            if (!gameResult.IsSuccess)
                return Response<LocalStoredAdvancedStatsEntryDto>.ErrorResponseFromKeyMessage(gameResult.ErrorMsg, ValidationKeys.Game);

            var advancedStatsEntry = Domain.AdvancedStatsEntries.AdvancedStatsEntry.Create(
                playerResult.Value.Id,
                teamResult.Value.Id,
                gameResult.Value.Id,
                request.Pie,
                request.Pace,
                request.AssistPercentage,
                request.AssistRatio,
                request.AssistToTurnover,
                request.DefensiveRating,
                request.DefensiveReboundPercentage,
                request.EffectiveFieldGoalPercentage,
                request.NetRating,
                request.OffensiveRating,
                request.OffensiveReboundPercentage,
                request.ReboundPercentage,
                request.TrueShootingPercentage,
                request.TurnoverRatio,
                request.UsagePercentage
            );

            var addResult = await _advancedStatsEntryRepository.AddAsync(advancedStatsEntry);
            if (!addResult.IsSuccess)
                return Response<LocalStoredAdvancedStatsEntryDto>.ErrorResponseFromKeyMessage(addResult.ErrorMsg, ValidationKeys.AdvancedStatsEntry);

            var afterAddResult = await _advancedStatsEntryRepository.FindByIdIncludingAll(addResult.Value.Id);
            if (!afterAddResult.IsSuccess)
                return Response<LocalStoredAdvancedStatsEntryDto>.ErrorResponseFromKeyMessage(afterAddResult.ErrorMsg, ValidationKeys.AdvancedStatsEntry);

            return new Response<LocalStoredAdvancedStatsEntryDto>
            {
                Success = true,
                Data = _localAdvancedStatsMapper.AdvancedStatsToLocalAdvancedStatsEntryDto(afterAddResult.Value, isLicensed)
            };
        }
    }
}
