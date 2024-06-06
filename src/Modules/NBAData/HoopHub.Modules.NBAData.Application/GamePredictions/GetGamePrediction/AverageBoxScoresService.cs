using HoopHub.Modules.NBAData.Application.GamePredictions.Models;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.BoxScores;

namespace HoopHub.Modules.NBAData.Application.GamePredictions.GetGamePrediction
{
    public class AverageBoxScoresService
    {
        public static async Task<(List<AverageBoxScores> SeasonAverages, List<AverageBoxScores> Last5GamesAverages)> GetBoxScoresAveragesSinceSeasonStartForTeam(
            IBoxScoresRepository boxScoresRepository,
            int teamApiId,
            string date)
        {
            var gameDate = DateTime.Parse(date).ToUniversalTime();
            var seasonStart = GetSeasonStartDate(gameDate);

            var boxScoresResult = await boxScoresRepository.GetBoxScoresForTeamSinceStartOfSeason(seasonStart, gameDate, teamApiId);
            var boxScores = boxScoresResult.Value;

            var seasonAverages = ComputeAverages(boxScores);
            var last5GamesAverages = ComputeAveragesForLast5Games(boxScores);

            var sortedAverages = SortByMinutes(last5GamesAverages, seasonAverages);
            return (sortedAverages.SeasonAverages, sortedAverages.Last5GamesAverages);
        }

        private static List<AverageBoxScores> ComputeAverages(IEnumerable<BoxScores> boxScores)
        {
            var playerTotals = new Dictionary<int, AverageBoxScores>();
            var playerGameCounts = new Dictionary<int, int>();

            foreach (var boxScore in boxScores)
            {
                var playerMinutes = (float)ParseMinutes(boxScore.Min);
                if (playerMinutes == 0)
                    continue;

                if (!playerTotals.ContainsKey(boxScore.Player.ApiId))
                {
                    playerTotals[boxScore.Player.ApiId] = new AverageBoxScores();
                    playerGameCounts[boxScore.Player.ApiId] = 0;
                }


                var playerStats = playerTotals[boxScore.Player.ApiId];
                playerStats.Min += playerMinutes;
                playerStats.Fgm += boxScore.Fgm ?? 0;
                playerStats.Fga += boxScore.Fga ?? 0;
                playerStats.FgPct += boxScore.FgPct.HasValue ? (float)boxScore.FgPct! : 0;
                playerStats.Fg3m += boxScore.Fg3m ?? 0;
                playerStats.Fg3a += boxScore.Fg3a ?? 0;
                playerStats.Fg3Pct += boxScore.Fg3Pct.HasValue ? (float)boxScore.Fg3Pct! : 0;
                playerStats.Ftm += boxScore.Ftm ?? 0;
                playerStats.Fta += boxScore.Fta ?? 0;
                playerStats.FtPct += boxScore.FtPct.HasValue ? (float)boxScore.FtPct! : 0;
                playerStats.Oreb += boxScore.Oreb ?? 0;
                playerStats.Dreb += boxScore.Dreb ?? 0;
                playerStats.Reb += boxScore.Reb ?? 0;
                playerStats.Ast += boxScore.Ast ?? 0;
                playerStats.Stl += boxScore.Stl ?? 0;
                playerStats.Blk += boxScore.Blk ?? 0;
                playerStats.Turnover += boxScore.Turnover ?? 0;
                playerStats.Pf += boxScore.Pf ?? 0;
                playerStats.Pts += boxScore.Pts ?? 0;
                playerGameCounts[boxScore.Player.ApiId]++;
                playerTotals[boxScore.Player.ApiId] = playerStats;
            }

            return playerTotals.Select(kv => new AverageBoxScores
            {
                PlayerId = kv.Key,
                Min = kv.Value.Min / playerGameCounts[kv.Key],
                Fgm = kv.Value.Fgm / playerGameCounts[kv.Key],
                Fga = kv.Value.Fga / playerGameCounts[kv.Key],
                FgPct = kv.Value.FgPct / playerGameCounts[kv.Key],
                Fg3m = kv.Value.Fg3m / playerGameCounts[kv.Key],
                Fg3a = kv.Value.Fg3a / playerGameCounts[kv.Key],
                Fg3Pct = kv.Value.Fg3Pct / playerGameCounts[kv.Key],
                Ftm = kv.Value.Ftm / playerGameCounts[kv.Key],
                Fta = kv.Value.Fta / playerGameCounts[kv.Key],
                FtPct = kv.Value.FtPct / playerGameCounts[kv.Key],
                Oreb = kv.Value.Oreb / playerGameCounts[kv.Key],
                Dreb = kv.Value.Dreb / playerGameCounts[kv.Key],
                Reb = kv.Value.Reb / playerGameCounts[kv.Key],
                Ast = kv.Value.Ast / playerGameCounts[kv.Key],
                Stl = kv.Value.Stl / playerGameCounts[kv.Key],
                Blk = kv.Value.Blk / playerGameCounts[kv.Key],
                Turnover = kv.Value.Turnover / playerGameCounts[kv.Key],
                Pf = kv.Value.Pf / playerGameCounts[kv.Key],
                Pts = kv.Value.Pts / playerGameCounts[kv.Key]
            }).ToList();
        }

        private static IEnumerable<AverageBoxScores> ComputeAveragesForLast5Games(IReadOnlyList<BoxScores> boxScores)
        {
            var gamesGroupedByDate = boxScores.GroupBy(bs => bs.Game.Date).OrderByDescending(g => g.Key).Take(5);
            var last5GamesBoxScores = gamesGroupedByDate.SelectMany(g => g).ToList();

            var playerTotals = new Dictionary<int, AverageBoxScores>();

            foreach (var boxScore in last5GamesBoxScores)
            {
                if (!playerTotals.ContainsKey(boxScore.Player.ApiId))
                {
                    playerTotals[boxScore.Player.ApiId] = new AverageBoxScores();
                }

                var playerStats = playerTotals[boxScore.Player.ApiId];
                playerStats.Min += (float)ParseMinutes(boxScore.Min);
                playerStats.Fgm += boxScore.Fgm ?? 0;
                playerStats.Fga += boxScore.Fga ?? 0;
                playerStats.FgPct += boxScore.FgPct.HasValue ? (float)boxScore.FgPct! : 0;
                playerStats.Fg3m += boxScore.Fg3m ?? 0;
                playerStats.Fg3a += boxScore.Fg3a ?? 0;
                playerStats.Fg3Pct += boxScore.Fg3Pct.HasValue ? (float)boxScore.Fg3Pct! : 0;
                playerStats.Ftm += boxScore.Ftm ?? 0;
                playerStats.Fta += boxScore.Fta ?? 0;
                playerStats.FtPct += boxScore.FtPct.HasValue ? (float)boxScore.FtPct! : 0;
                playerStats.Oreb += boxScore.Oreb ?? 0;
                playerStats.Dreb += boxScore.Dreb ?? 0;
                playerStats.Reb += boxScore.Reb ?? 0;
                playerStats.Ast += boxScore.Ast ?? 0;
                playerStats.Stl += boxScore.Stl ?? 0;
                playerStats.Blk += boxScore.Blk ?? 0;
                playerStats.Turnover += boxScore.Turnover ?? 0;
                playerStats.Pf += boxScore.Pf ?? 0;
                playerStats.Pts += boxScore.Pts ?? 0;
                playerTotals[boxScore.Player.ApiId] = playerStats;
            }

            foreach (var playerId in boxScores.Select(bs => bs.Player.ApiId).Distinct())
            {
                if (!playerTotals.ContainsKey(playerId))
                {
                    playerTotals[playerId] = new AverageBoxScores();
                }
            }

            return playerTotals.Select(kv => new AverageBoxScores
            {
                PlayerId = kv.Key,
                Min = kv.Value.Min / 5,
                Fgm = kv.Value.Fgm / 5,
                Fga = kv.Value.Fga / 5,
                FgPct = kv.Value.FgPct / 5,
                Fg3m = kv.Value.Fg3m / 5,
                Fg3a = kv.Value.Fg3a / 5,
                Fg3Pct = kv.Value.Fg3Pct / 5,
                Ftm = kv.Value.Ftm / 5,
                Fta = kv.Value.Fta / 5,
                FtPct = kv.Value.FtPct / 5,
                Oreb = kv.Value.Oreb / 5,
                Dreb = kv.Value.Dreb / 5,
                Reb = kv.Value.Reb / 5,
                Ast = kv.Value.Ast / 5,
                Stl = kv.Value.Stl / 5,
                Blk = kv.Value.Blk / 5,
                Turnover = kv.Value.Turnover / 5,
                Pf = kv.Value.Pf / 5,
                Pts = kv.Value.Pts / 5
            }).ToList();
        }

        public static DateTime GetSeasonStartDate(DateTime gameDate)
        {
            return gameDate.Month >= 9 ? new DateTime(gameDate.Year, 9, 1).ToUniversalTime() : new DateTime(gameDate.Year - 1, 9, 1).ToUniversalTime();
        }

        private static (List<AverageBoxScores> SeasonAverages, List<AverageBoxScores> Last5GamesAverages) SortByMinutes(
            IEnumerable<AverageBoxScores> last5Games, IReadOnlyCollection<AverageBoxScores> seasonAverages)
        {
            var sortedLast5Games = last5Games.OrderByDescending(bs => bs.Min).ToList();
            var playerIdsInSortedOrder = sortedLast5Games.Select(bs => bs.PlayerId).ToList();

            var sortedSeasonAverages = playerIdsInSortedOrder
                .Select(playerId => seasonAverages.First(p => p.PlayerId == playerId))
                .ToList();

            return (sortedSeasonAverages, sortedLast5Games);
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
