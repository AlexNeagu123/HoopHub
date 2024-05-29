using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Standings.Dtos;
using HoopHub.Modules.NBAData.Application.Standings.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Standings.GetStandingsBySeason
{
    public class GetStandingsBySeasonQueryHandler(IStandingsRepository standingsRepository, ICurrentUserService currentUserService) : IRequestHandler<GetStandingsBySeasonQuery, Response<List<StandingsEntryDto>>>
    {
        private readonly IStandingsRepository _standingsRepository = standingsRepository;
        private readonly StandingsMapper _standingsMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;


        public async Task<Response<List<StandingsEntryDto>>> Handle(GetStandingsBySeasonQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetStandingsBySeasonQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<List<StandingsEntryDto>>.ErrorResponseFromFluentResult(validationResult);

            var nextSeasonString = (request.Season + 1).ToString();
            var seasonPeriod = $"{request.Season}-{nextSeasonString[^2..]}";

            var queryResult = await _standingsRepository.GetStandingsBySeasonPeriod(seasonPeriod);
            if (!queryResult.IsSuccess)
                return Response<List<StandingsEntryDto>>.ErrorResponseFromKeyMessage(queryResult.ErrorMsg, ValidationKeys.SeasonPeriod);

            var standings = queryResult.Value;
            var standingsDtoList = standings.Select(s => _standingsMapper.StandingsEntryToStandingsEntryDto(s, isLicensed)).ToList();

            return new Response<List<StandingsEntryDto>>()
            {
                Success = true,
                Data = standingsDtoList
            };
        }
    }
}
