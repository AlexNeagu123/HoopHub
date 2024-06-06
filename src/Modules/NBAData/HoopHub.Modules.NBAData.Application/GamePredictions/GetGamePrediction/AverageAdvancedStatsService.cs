using HoopHub.Modules.NBAData.Application.GamePredictions.Models;
using HoopHub.Modules.NBAData.Application.Persistence;

namespace HoopHub.Modules.NBAData.Application.GamePredictions.GetGamePrediction
{
    public class AverageAdvancedStatsService
    {
        public static async Task<(List<AverageAdvancedStats> SeasonAverages, List<AverageAdvancedStats> Last5GamesAverages)> GetAdvancedStatsAveragesSinceSeasonStartForTeam(
            IAdvancedStatsEntryRepository advancedStatsRepository,
            IBoxScoresRepository boxScoresRepository,
            int teamApiId,
            string date)
        {
            var gameDate = DateTime.Parse(date).ToUniversalTime();
            var seasonStart = GetSeasonStartDate(gameDate);

            var advancedStatsResult = await advancedStatsRepository.GetBoxScoresForTeamSinceStartOfSeason(seasonStart, gameDate, teamApiId);
            var advancedStats = advancedStatsResult.Value;

            var boxScoresResult = await boxScoresRepository.GetBoxScoresForTeamSinceStartOfSeason(seasonStart, gameDate, teamApiId);
            var boxScores = boxScoresResult.Value;

            var playerMinutesPerGame = boxScores
                .GroupBy(bs => new Tuple<Guid, int>(bs.GameId, bs.Player.ApiId))
                .ToDictionary(
                    g => g.Key,
                    g => (float)ParseMinutes(g.First().Min)
                );

            var seasonAverages = ComputeAverages(advancedStats, playerMinutesPerGame);
            var last5GamesAverages = ComputeAveragesForLast5Games(advancedStats);

            return (seasonAverages, last5GamesAverages);
        }

        private static List<AverageAdvancedStats> ComputeAverages(IEnumerable<Domain.AdvancedStatsEntries.AdvancedStatsEntry> advancedStats, 
            Dictionary<Tuple<Guid, int>, float> playerMinutesPerGame)
        {
            var playerTotals = new Dictionary<int, AverageAdvancedStats>();
            var playerGameCounts = new Dictionary<int, int>();

            foreach (var stat in advancedStats)
            {
                var key = new Tuple<Guid, int>(stat.GameId, stat.Player.ApiId);
                if (!playerMinutesPerGame.TryGetValue(key, out var minutes) || minutes == 0)
                    continue;

                if (!playerTotals.ContainsKey(stat.Player.ApiId))
                {
                    playerTotals[stat.Player.ApiId] = new AverageAdvancedStats();
                    playerGameCounts[stat.Player.ApiId] = 0;
                }

                var playerStats = playerTotals[stat.Player.ApiId];
                playerStats.Pie += stat.Pie ?? 0;
                playerStats.Pace += stat.Pace ?? 0;
                playerStats.AssistPercentage += stat.AssistPercentage ?? 0;
                playerStats.AssistRatio += stat.AssistRatio ?? 0;
                playerStats.AssistToTurnover += stat.AssistToTurnover ?? 0;
                playerStats.DefensiveRating += stat.DefensiveRating ?? 0;
                playerStats.DefensiveReboundPercentage += stat.DefensiveReboundPercentage ?? 0;
                playerStats.EffectiveFieldGoalPercentage += stat.EffectiveFieldGoalPercentage ?? 0;
                playerStats.NetRating += stat.NetRating ?? 0;
                playerStats.OffensiveRating += stat.OffensiveRating ?? 0;
                playerStats.OffensiveReboundPercentage += stat.OffensiveReboundPercentage ?? 0;
                playerStats.ReboundPercentage += stat.ReboundPercentage ?? 0;
                playerStats.TrueShootingPercentage += stat.TrueShootingPercentage ?? 0;
                playerStats.TurnoverRatio += stat.TurnoverRatio ?? 0;
                playerStats.UsagePercentage += stat.UsagePercentage ?? 0;
                playerGameCounts[stat.Player.ApiId]++;
                playerTotals[stat.Player.ApiId] = playerStats;
            }

            return playerTotals.Select(kv => new AverageAdvancedStats
            {
                PlayerId = kv.Key,
                Pie = kv.Value.Pie / playerGameCounts[kv.Key],
                Pace = kv.Value.Pace / playerGameCounts[kv.Key],
                AssistPercentage = kv.Value.AssistPercentage / playerGameCounts[kv.Key],
                AssistRatio = kv.Value.AssistRatio / playerGameCounts[kv.Key],
                AssistToTurnover = kv.Value.AssistToTurnover / playerGameCounts[kv.Key],
                DefensiveRating = kv.Value.DefensiveRating / playerGameCounts[kv.Key],
                DefensiveReboundPercentage = kv.Value.DefensiveReboundPercentage / playerGameCounts[kv.Key],
                EffectiveFieldGoalPercentage = kv.Value.EffectiveFieldGoalPercentage / playerGameCounts[kv.Key],
                NetRating = kv.Value.NetRating / playerGameCounts[kv.Key],
                OffensiveRating = kv.Value.OffensiveRating / playerGameCounts[kv.Key],
                OffensiveReboundPercentage = kv.Value.OffensiveReboundPercentage / playerGameCounts[kv.Key],
                ReboundPercentage = kv.Value.ReboundPercentage / playerGameCounts[kv.Key],
                TrueShootingPercentage = kv.Value.TrueShootingPercentage / playerGameCounts[kv.Key],
                TurnoverRatio = kv.Value.TurnoverRatio / playerGameCounts[kv.Key],
                UsagePercentage = kv.Value.UsagePercentage / playerGameCounts[kv.Key]
            }).ToList();
        }

        private static List<AverageAdvancedStats> ComputeAveragesForLast5Games(IReadOnlyList<Domain.AdvancedStatsEntries.AdvancedStatsEntry> advancedStats)
        {
            var gamesGroupedByDate = advancedStats.GroupBy(bs => bs.Game.Date).OrderByDescending(g => g.Key).Take(5);
            var last5GamesAdvancedStats = gamesGroupedByDate.SelectMany(g => g).ToList();

            var playerTotals = new Dictionary<int, AverageAdvancedStats>();

            foreach (var stat in last5GamesAdvancedStats)
            {
                if (!playerTotals.ContainsKey(stat.Player.ApiId))
                {
                    playerTotals[stat.Player.ApiId] = new AverageAdvancedStats();
                }

                var playerStats = playerTotals[stat.Player.ApiId];
                playerStats.Pie += stat.Pie ?? 0;
                playerStats.Pace += stat.Pace ?? 0;
                playerStats.AssistPercentage += stat.AssistPercentage ?? 0;
                playerStats.AssistRatio += stat.AssistRatio ?? 0;
                playerStats.AssistToTurnover += stat.AssistToTurnover ?? 0;
                playerStats.DefensiveRating += stat.DefensiveRating ?? 0;
                playerStats.DefensiveReboundPercentage += stat.DefensiveReboundPercentage ?? 0;
                playerStats.EffectiveFieldGoalPercentage += stat.EffectiveFieldGoalPercentage ?? 0;
                playerStats.NetRating += stat.NetRating ?? 0;
                playerStats.OffensiveRating += stat.OffensiveRating ?? 0;
                playerStats.OffensiveReboundPercentage += stat.OffensiveReboundPercentage ?? 0;
                playerStats.ReboundPercentage += stat.ReboundPercentage ?? 0;
                playerStats.TrueShootingPercentage += stat.TrueShootingPercentage ?? 0;
                playerStats.TurnoverRatio += stat.TurnoverRatio ?? 0;
                playerStats.UsagePercentage += stat.UsagePercentage ?? 0;
                playerTotals[stat.Player.ApiId] = playerStats;
            }

            foreach (var playerId in advancedStats.Select(bs => bs.Player.ApiId).Distinct())
            {
                if (!playerTotals.ContainsKey(playerId))
                {
                    playerTotals[playerId] = new AverageAdvancedStats();
                }
            }

            return playerTotals.Select(kv => new AverageAdvancedStats
            {
                PlayerId = kv.Key,
                Pie = kv.Value.Pie / 5,
                Pace = kv.Value.Pace / 5,
                AssistPercentage = kv.Value.AssistPercentage / 5,
                AssistRatio = kv.Value.AssistRatio / 5,
                AssistToTurnover = kv.Value.AssistToTurnover / 5,
                DefensiveRating = kv.Value.DefensiveRating / 5,
                DefensiveReboundPercentage = kv.Value.DefensiveReboundPercentage / 5,
                EffectiveFieldGoalPercentage = kv.Value.EffectiveFieldGoalPercentage / 5,
                NetRating = kv.Value.NetRating / 5,
                OffensiveRating = kv.Value.OffensiveRating / 5,
                OffensiveReboundPercentage = kv.Value.OffensiveReboundPercentage / 5,
                ReboundPercentage = kv.Value.ReboundPercentage / 5,
                TrueShootingPercentage = kv.Value.TrueShootingPercentage / 5,
                TurnoverRatio = kv.Value.TurnoverRatio / 5,
                UsagePercentage = kv.Value.UsagePercentage / 5
            }).ToList();
        }

        public static DateTime GetSeasonStartDate(DateTime gameDate)
        {
            return gameDate.Month >= 9 ? new DateTime(gameDate.Year, 9, 1).ToUniversalTime() : new DateTime(gameDate.Year - 1, 9, 1).ToUniversalTime();
        }

        private static double ParseMinutes(string? min)
        {
            if (string.IsNullOrEmpty(min))
                return 0;

            var parts = min.Split(':');
            int minutes, seconds;

            switch (parts.Length)
            {
                case 1 when int.TryParse(parts[0], out minutes):
                    seconds = 0;
                    break;
                case 1:
                    throw new ArgumentException("Invalid time format");
                case 2 when int.TryParse(parts[0], out minutes) && int.TryParse(parts[1], out seconds):
                    break;
                case 2:
                    throw new ArgumentException("Invalid time format");
                default:
                    throw new ArgumentException("Invalid time format");
            }

            return minutes + (float)seconds / 60;
        }
    }
}
