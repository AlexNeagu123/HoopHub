using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Playoffs.Dtos;
using HoopHub.Modules.NBAData.Application.Playoffs.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Playoffs.GetPlayoffSeriesBySeason
{
    public class GetPlayoffSeriesBySeasonQueryHandler(IPlayoffSeriesRepository playoffSeriesRepository, ICurrentUserService currentUserService) : IRequestHandler<GetPlayoffSeriesBySeasonQuery, Response<GroupedPlayoffSeriesDto>>
    {
        private readonly IPlayoffSeriesRepository _playoffSeriesRepository = playoffSeriesRepository;
        private readonly PlayoffSeriesMapper _playoffSeriesMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<GroupedPlayoffSeriesDto>> Handle(GetPlayoffSeriesBySeasonQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetPlayoffSeriesBySeasonQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<GroupedPlayoffSeriesDto>.ErrorResponseFromFluentResult(validationResult);


            var nextSeasonString = (request.Season + 1).ToString();
            var seasonPeriod = $"{request.Season}-{nextSeasonString[^2..]}";
            var playoffSeries = await _playoffSeriesRepository.GetPlayoffSeriesBySeasonAsync(seasonPeriod);

            if (!playoffSeries.IsSuccess)
                return Response<GroupedPlayoffSeriesDto>.ErrorResponseFromKeyMessage(playoffSeries.ErrorMsg, ValidationKeys.PlayoffSeries);

            var playoffSeriesDtoList = playoffSeries.Value.Select(series => _playoffSeriesMapper.PlayoffSeriesToPlayoffSeriesDto(series, isLicensed)).ToList();

            var priorityIndexes = new Dictionary<Guid, int>();
            foreach (var seriesDto in playoffSeriesDtoList)
            {
                if (seriesDto.Stage != Config.EasternFirstRound &&
                    seriesDto.Stage != Config.WesternFirstRound) continue;

                var minRank = Math.Min(seriesDto.WinningTeamRank, seriesDto.LosingTeamRank);
                priorityIndexes.Add(seriesDto.WinningTeam.Id, minRank);
                priorityIndexes.Add(seriesDto.LosingTeam.Id, minRank);
            }

            var stageGrouping = playoffSeriesDtoList
                .GroupBy(seriesDto => seriesDto.Stage)
                .ToDictionary(
                    group => group.Key,
                    group =>
                    {
                        return group.OrderBy(series => Math.Min(priorityIndexes.GetValueOrDefault(series.WinningTeam.Id, int.MaxValue),
                                priorityIndexes.GetValueOrDefault(series.LosingTeam.Id, int.MaxValue)))
                            .ToList();
                    }
                );

            return new Response<GroupedPlayoffSeriesDto>
            {
                Success = true,
                Data = new GroupedPlayoffSeriesDto
                {
                    StageGrouping = stageGrouping
                }
            };
        }
    }
}
