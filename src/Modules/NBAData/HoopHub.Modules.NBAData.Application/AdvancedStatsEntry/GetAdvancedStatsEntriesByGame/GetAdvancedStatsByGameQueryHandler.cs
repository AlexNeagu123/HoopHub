using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Dtos;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Mappers;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.GetAdvancedStatsEntriesByGame
{
    public class GetAdvancedStatsByGameQueryHandler(
        IAdvancedStatsEntryRepository advancedStatsEntryRepository,
        IGameRepository gameRepository,
        ITeamRepository teamRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetAdvancedStatsByGameQuery, Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>>
    {
        private readonly IAdvancedStatsEntryRepository _advancedStatsEntryRepository = advancedStatsEntryRepository;
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly LocalAdvancedStatsMapper _localAdvancedStatsMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>> Handle(GetAdvancedStatsByGameQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetAdvancedStatsByGameQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromFluentResult(validationResult);

            var homeTeamResult = await _teamRepository.FindByApiIdAsync(request.HomeTeamApiId);
            if (!homeTeamResult.IsSuccess)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromKeyMessage(
                    homeTeamResult.ErrorMsg, ValidationKeys.TeamApiId);

            var visitorTeamResult = await _teamRepository.FindByApiIdAsync(request.VisitorTeamApiId);
            if (!visitorTeamResult.IsSuccess)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromKeyMessage(
                                              visitorTeamResult.ErrorMsg, ValidationKeys.TeamApiId);

            var homeTeam = homeTeamResult.Value;
            var visitorTeam = visitorTeamResult.Value;
            var gameResult = await
                _gameRepository.FindGameByDateAndTeams(DateTime.Parse(request.Date).ToUniversalTime(), homeTeam.Id, visitorTeam.Id);

            if (!gameResult.IsSuccess)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromKeyMessage(
                                       gameResult.ErrorMsg, ValidationKeys.Game);

            var game = gameResult.Value;
            var advancedStatsEntriesResult = await _advancedStatsEntryRepository.GetByGameAsync(game.Id);
            if (!advancedStatsEntriesResult.IsSuccess)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromKeyMessage(
                                       advancedStatsEntriesResult.ErrorMsg, ValidationKeys.AdvancedStatsEntry);

            var advancedStatsEntries = advancedStatsEntriesResult.Value;
            var advancedStatsEntriesDtoList = advancedStatsEntries.Select(ase => _localAdvancedStatsMapper.AdvancedStatsToLocalAdvancedStatsEntryDto(ase, isLicensed)).ToList();

            return new Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>
            {
                Success = true,
                Data = advancedStatsEntriesDtoList
            };
        }
    }
}
