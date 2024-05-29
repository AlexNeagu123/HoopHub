using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Dtos;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.GetAdvancedStatsEntriesByPlayer
{
    public class GetAdvancedStatsEntriesByPlayerQueryHandler(IAdvancedStatsEntryRepository advancedStatsEntryRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetAdvancedStatsEntriesByPlayerQuery, Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>>
    {
        private readonly IAdvancedStatsEntryRepository _advancedStatsEntryRepository = advancedStatsEntryRepository;
        private readonly LocalAdvancedStatsMapper _advancedStatsMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;
        public async Task<Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>> Handle(GetAdvancedStatsEntriesByPlayerQuery request,
            CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetAdvancedStatsEntriesByPlayerQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromFluentResult(validationResult);

            var advancedStatsEntries = await _advancedStatsEntryRepository.GetLastXAdvancedStatsByPlayerId(request.PlayerId, 5);
            if (!advancedStatsEntries.IsSuccess)
                return Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>.ErrorResponseFromKeyMessage(advancedStatsEntries.ErrorMsg, ValidationKeys.AdvancedStatsEntry);

            var advancedStatsDtoList = advancedStatsEntries.Value.Select(ase => _advancedStatsMapper.AdvancedStatsToLocalAdvancedStatsEntryDto(ase, isLicensed)).ToList();
            return new Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>
            {
                Success = true,
                Data = advancedStatsDtoList
            };
        }
    }
}
