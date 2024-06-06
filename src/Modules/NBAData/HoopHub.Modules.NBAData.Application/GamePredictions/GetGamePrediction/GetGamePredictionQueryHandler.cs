using HoopHub.BuildingBlocks.Application.ExternalServices.PredictionsModel;
using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.GamePredictions.Dtos;
using HoopHub.Modules.NBAData.Application.GamePredictions.Models;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.GamePredictions.GetGamePrediction
{
    public class GetGamePredictionQueryHandler(
        ITensorFlowModelService tensorFlowModelService,
        IGameRepository gameRepository,
        IAdvancedStatsEntryRepository advancedStatsEntryRepository,
        IBoxScoresRepository boxScoresRepository,
        IPlayerRepository playerRepository)
        : IRequestHandler<GetGamePredictionQuery, Response<GamePredictionDto>>
    {
        private readonly ITensorFlowModelService _tensorFlowModelService = tensorFlowModelService;
        private readonly IAdvancedStatsEntryRepository _advancedStatsEntryRepository = advancedStatsEntryRepository;
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly IBoxScoresRepository _boxScoresRepository = boxScoresRepository;
        private const int PlayersCount = 7;

        public async Task<Response<GamePredictionDto>> Handle(GetGamePredictionQuery request,
            CancellationToken cancellationToken)
        {
            var validator = new GetGamePredictionQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<GamePredictionDto>.ErrorResponseFromFluentResult(validationResult);

            var (homeTeamBoxScoresAverages, homeTeamLast5BoxScoresAverages) =
                await AverageBoxScoresService.GetBoxScoresAveragesSinceSeasonStartForTeam(_boxScoresRepository, request.HomeTeamId, request.Date);
            var (visitorTeamBoxScoresAverages, visitorTeamLast5BoxScoresAverages) =
                await AverageBoxScoresService.GetBoxScoresAveragesSinceSeasonStartForTeam(_boxScoresRepository, request.VisitorTeamId, request.Date);

            var (homeTeamAdvancedStatsAverages, homeTeamLast5AdvancedStatsAverages) =
                await AverageAdvancedStatsService.GetAdvancedStatsAveragesSinceSeasonStartForTeam(
                    _advancedStatsEntryRepository, _boxScoresRepository, request.HomeTeamId, request.Date);

            var (visitorTeamAdvancedStatsAverages, visitorTeamLast5AdvancedStatsAverages) =
                await AverageAdvancedStatsService.GetAdvancedStatsAveragesSinceSeasonStartForTeam(
                    _advancedStatsEntryRepository, _boxScoresRepository, request.VisitorTeamId, request.Date);

            var homeTeamLast5PlayerOrder = homeTeamLast5BoxScoresAverages.Select(bs => bs.PlayerId).ToList();
            var visitorTeamLast5PlayerOrder = visitorTeamLast5BoxScoresAverages.Select(bs => bs.PlayerId).ToList();

            homeTeamAdvancedStatsAverages =
                SortAdvancedStatsByPlayerOrder(homeTeamAdvancedStatsAverages, homeTeamLast5PlayerOrder);

            visitorTeamAdvancedStatsAverages =
                SortAdvancedStatsByPlayerOrder(visitorTeamAdvancedStatsAverages, visitorTeamLast5PlayerOrder);

            homeTeamLast5AdvancedStatsAverages = SortAdvancedStatsByPlayerOrder(homeTeamLast5AdvancedStatsAverages, homeTeamLast5PlayerOrder);
            visitorTeamLast5AdvancedStatsAverages = SortAdvancedStatsByPlayerOrder(visitorTeamLast5AdvancedStatsAverages, visitorTeamLast5PlayerOrder);

            var homeTeamAverageStats =
                await AverageTeamStatsService.GetSeasonAverages(_gameRepository, request.HomeTeamId, request.Date);

            var homeTeamAverageLast5Stats =
                await AverageTeamStatsService.GetLast5GamesAverages(_gameRepository, request.HomeTeamId, request.Date);


            var visitorTeamAverageStats =
                await AverageTeamStatsService.GetSeasonAverages(_gameRepository, request.VisitorTeamId, request.Date);

            var visitorTeamAverageLast5Stats =
                await AverageTeamStatsService.GetLast5GamesAverages(_gameRepository, request.VisitorTeamId, request.Date);


            var inputArray = CreateInputArray(
                homeTeamAverageStats,
                homeTeamAverageLast5Stats,
                visitorTeamAverageStats,
                visitorTeamAverageLast5Stats,
                homeTeamBoxScoresAverages,
                homeTeamLast5BoxScoresAverages,
                visitorTeamBoxScoresAverages,
                visitorTeamLast5BoxScoresAverages,
                homeTeamAdvancedStatsAverages,
                homeTeamLast5AdvancedStatsAverages,
                visitorTeamAdvancedStatsAverages,
                visitorTeamLast5AdvancedStatsAverages);


            var predictionsResult = await _tensorFlowModelService.PredictAsync(inputArray);
            if (!predictionsResult.IsSuccess)
                return Response<GamePredictionDto>.ErrorResponseFromKeyMessage(predictionsResult.ErrorMsg, ValidationKeys.Predictions);

            var predictions = predictionsResult.Value;

            return new Response<GamePredictionDto>
            {
                Success = true,
                Data = new GamePredictionDto
                {
                    HomeTeamWinProbability = predictions[0],
                    VisitorTeamWinProbability = predictions[1]
                }
            };
        }

        private static List<AverageAdvancedStats> SortAdvancedStatsByPlayerOrder(IReadOnlyCollection<AverageAdvancedStats> advancedStats, IEnumerable<int> playerOrder)
        {
            return playerOrder.Select(playerId => advancedStats.FirstOrDefault(aas => aas.PlayerId == playerId)).OfType<AverageAdvancedStats>().ToList();
        }

        private static float[] CreateInputArray(
            AverageTeamStatistics homeTeamAverageStats,
            AverageTeamStatistics homeTeamAverageLast5Stats,
            AverageTeamStatistics visitorTeamAverageStats,
            AverageTeamStatistics visitorTeamAverageLast5Stats,
            IReadOnlyList<AverageBoxScores> homeTeamSeasonAverages,
            IReadOnlyList<AverageBoxScores> homeTeamLast5GamesAverages,
            IReadOnlyList<AverageBoxScores> visitorTeamSeasonAverages,
            IReadOnlyList<AverageBoxScores> visitorTeamLast5GamesAverages,
            IReadOnlyList<AverageAdvancedStats> homeTeamAdvancedAverages,
            IReadOnlyList<AverageAdvancedStats> homeTeamLast5AdvancedAverages,
            IReadOnlyList<AverageAdvancedStats> visitorTeamAdvancedAverages,
            IReadOnlyList<AverageAdvancedStats> visitorTeamLast5AdvancedAverages)
        {
            var inputArray = new List<float>();
            inputArray.AddRange(new[]
            {
                homeTeamAverageStats.Streak, visitorTeamAverageStats.Streak,
                homeTeamAverageStats.WinPercentage, visitorTeamAverageStats.WinPercentage,
                homeTeamAverageStats.Points, visitorTeamAverageStats.Points,
                homeTeamAverageLast5Stats.Points, visitorTeamAverageLast5Stats.Points,
                homeTeamAverageStats.Assists, visitorTeamAverageStats.Assists,
                homeTeamAverageLast5Stats.Assists, visitorTeamAverageLast5Stats.Assists,
                homeTeamAverageStats.Rebounds, visitorTeamAverageStats.Rebounds,
                homeTeamAverageLast5Stats.Rebounds, visitorTeamAverageLast5Stats.Rebounds,
                homeTeamAverageStats.Steals, visitorTeamAverageStats.Steals,
                homeTeamAverageLast5Stats.Steals, visitorTeamAverageLast5Stats.Steals,
                homeTeamAverageStats.Blocks, visitorTeamAverageStats.Blocks,
                homeTeamAverageLast5Stats.Blocks, visitorTeamAverageLast5Stats.Blocks,
                homeTeamAverageStats.Turnovers, visitorTeamAverageStats.Turnovers,
                homeTeamAverageLast5Stats.Turnovers, visitorTeamAverageLast5Stats.Turnovers
            });

            AddPlayerStatistics(inputArray, homeTeamSeasonAverages, homeTeamLast5GamesAverages, homeTeamAdvancedAverages, homeTeamLast5AdvancedAverages);
            AddPlayerStatistics(inputArray, visitorTeamSeasonAverages, visitorTeamLast5GamesAverages, visitorTeamAdvancedAverages, visitorTeamLast5AdvancedAverages);

            return inputArray.ToArray();
        }

        private static void AddPlayerStatistics(
            List<float> inputArray,
            IReadOnlyList<AverageBoxScores> seasonAverageBoxScores,
            IReadOnlyList<AverageBoxScores> last5AverageBoxScores,
            IReadOnlyList<AverageAdvancedStats> advancedStatsAverages,
            IReadOnlyList<AverageAdvancedStats> last5AdvancedStatsAverages)
        {
            for (var i = 0; i < PlayersCount; i++)
            {
                var boxScore = i < seasonAverageBoxScores.Count ? seasonAverageBoxScores[i] : new AverageBoxScores();
                var last5BoxScore = i < last5AverageBoxScores.Count ? last5AverageBoxScores[i] : new AverageBoxScores();
                var advancedStat = i < advancedStatsAverages.Count ? advancedStatsAverages[i] : new AverageAdvancedStats();
                var last5AdvancedStat = i < last5AdvancedStatsAverages.Count ? last5AdvancedStatsAverages[i] : new AverageAdvancedStats();

                inputArray.AddRange(new[]
                {
                    boxScore.FgPct, last5BoxScore.FgPct, boxScore.Fg3Pct, last5BoxScore.Fg3Pct, boxScore.FtPct,
                    last5BoxScore.FtPct, boxScore.Oreb, boxScore.Dreb, boxScore.Reb, boxScore.Ast, last5BoxScore.Ast, boxScore.Stl,
                    boxScore.Blk, boxScore.Turnover, last5BoxScore.Turnover, boxScore.Pf, boxScore.Pts,
                    last5BoxScore.Pts, advancedStat.Pie, last5AdvancedStat.Pie, advancedStat.AssistPercentage, advancedStat.AssistRatio,
                    advancedStat.AssistToTurnover, last5AdvancedStat.AssistToTurnover, advancedStat.DefensiveRating,
                    last5AdvancedStat.DefensiveRating, advancedStat.DefensiveReboundPercentage, advancedStat.EffectiveFieldGoalPercentage,
                    last5AdvancedStat.EffectiveFieldGoalPercentage, advancedStat.NetRating, last5AdvancedStat.NetRating, advancedStat.OffensiveRating,
                    last5AdvancedStat.OffensiveRating, advancedStat.OffensiveReboundPercentage, advancedStat.ReboundPercentage, advancedStat.TrueShootingPercentage,
                    last5AdvancedStat.TrueShootingPercentage, advancedStat.TurnoverRatio, advancedStat.UsagePercentage, last5AdvancedStat.UsagePercentage
                });
            }
        }
    }
}