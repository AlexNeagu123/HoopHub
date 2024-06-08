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
        ICurrentUserService currentUserService)
        : IRequestHandler<GetAdvancedStatsByGameQuery, Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>>
    {
        private readonly IAdvancedStatsEntryRepository _advancedStatsEntryRepository = advancedStatsEntryRepository;
        private readonly LocalAdvancedStatsMapper _localAdvancedStatsMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>> Handle(GetAdvancedStatsByGameQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetAdvancedStatsByGameQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromFluentResult(validationResult);

            var convertedDate = DateTime.Parse(request.Date).ToUniversalTime();
            var advancedStatsResult = await _advancedStatsEntryRepository.GetAdvancedStatsByGameAsync(convertedDate, request.HomeTeamApiId, request.VisitorTeamApiId);

            if (advancedStatsResult.IsSuccess == false)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromKeyMessage(advancedStatsResult.ErrorMsg, ValidationKeys.BoxScores);

            var advancedStats = advancedStatsResult.Value;
            var advancedStatsEntriesDtoList = advancedStats.Select(bs => _localAdvancedStatsMapper.AdvancedStatsToLocalAdvancedStatsEntryDto(bs, isLicensed)).ToList();

            return new Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>
            {
                Success = true,
                Data = advancedStatsEntriesDtoList
            };
        }
    }
}
