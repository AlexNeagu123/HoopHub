using HoopHub.Modules.NBAData.Application.GamePredictions.Models;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Games;

namespace HoopHub.Modules.NBAData.Application.GamePredictions.GetGamePrediction;

public class AverageTeamStatsService
{

    public static async Task<AverageTeamStatistics> GetSeasonAverages(IGameRepository gameRepository, int teamApiId, string date)
    {
        var gameDate = DateTime.Parse(date).ToUniversalTime();
        var seasonStart = GetSeasonStartDate(gameDate);

        var gamesResult = await gameRepository.GetGamesForTeamSinceStartOfSeason(seasonStart, gameDate, teamApiId);
        var games = gamesResult.Value;

        return ComputeTeamStatistics(games, teamApiId);
    }

    public static async Task<AverageTeamStatistics> GetLast5GamesAverages(IGameRepository gameRepository, int teamApiId, string date)
    {
        var gameDate = DateTime.Parse(date).ToUniversalTime();
        var seasonStart = GetSeasonStartDate(gameDate);

        var gamesResult = await gameRepository.GetGamesForTeamSinceStartOfSeason(seasonStart, gameDate, teamApiId);
        var games = gamesResult.Value;

        var last5Games = games.OrderByDescending(g => g.Date).Take(5).ToList();
        return ComputeTeamStatistics(last5Games, teamApiId);
    }

    private static AverageTeamStatistics ComputeTeamStatistics(IReadOnlyCollection<Game> games, int teamApiId)
    {
        var totalGames = games.Count();
        var totalWins = games.Count(g => (g.HomeTeam.ApiId == teamApiId && g.HomeTeamScore > g.VisitorTeamScore) ||
                                         (g.VisitorTeam.ApiId == teamApiId && g.VisitorTeamScore > g.HomeTeamScore));

        var streak = 0;
        var isCurrentStreakActive = true;

        foreach (var game in games.OrderByDescending(g => g.Date))
        {
            var isWin = (game.HomeTeam.ApiId == teamApiId && game.HomeTeamScore > game.VisitorTeamScore) ||
                        (game.VisitorTeam.ApiId == teamApiId && game.VisitorTeamScore > game.HomeTeamScore);

            if (isWin && isCurrentStreakActive)
                streak++;
            else
                isCurrentStreakActive = false;
        }

        var points = games.Sum(g => g.BoxScores.Where(bs => bs.Team.ApiId == teamApiId).Sum(bs => bs.Pts ?? 0));
        var assists = games.Sum(g => g.BoxScores.Where(bs => bs.Team.ApiId == teamApiId).Sum(bs => bs.Ast ?? 0));
        var rebounds = games.Sum(g => g.BoxScores.Where(bs => bs.Team.ApiId == teamApiId).Sum(bs => bs.Reb ?? 0));
        var steals = games.Sum(g => g.BoxScores.Where(bs => bs.Team.ApiId == teamApiId).Sum(bs => bs.Stl ?? 0));
        var blocks = games.Sum(g => g.BoxScores.Where(bs => bs.Team.ApiId == teamApiId).Sum(bs => bs.Blk ?? 0));
        var turnovers = games.Sum(g => g.BoxScores.Where(bs => bs.Team.ApiId == teamApiId).Sum(bs => bs.Turnover ?? 0));

        return new AverageTeamStatistics
        {
            Streak = streak,
            WinPercentage = totalGames > 0 ? (float)totalWins / totalGames : 0,
            Points = totalGames > 0 ? (float)points / totalGames : 0,
            Assists = totalGames > 0 ? (float)assists / totalGames : 0,
            Rebounds = totalGames > 0 ? (float)rebounds / totalGames : 0,
            Steals = totalGames > 0 ? (float)steals / totalGames : 0,
            Blocks = totalGames > 0 ? (float)blocks / totalGames : 0,
            Turnovers = totalGames > 0 ? (float)turnovers / totalGames : 0
        };
    }

    private static DateTime GetSeasonStartDate(DateTime gameDate)
    {
        return gameDate.Month >= 9 ? new DateTime(gameDate.Year, 9, 1).ToUniversalTime() : new DateTime(gameDate.Year - 1, 9, 1).ToUniversalTime();
    }
}