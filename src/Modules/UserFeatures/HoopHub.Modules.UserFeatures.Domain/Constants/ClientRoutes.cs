using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Domain.Constants
{
    public class ClientRoutes
    {
        public const string BaseRoute = "http://localhost:5173";
        public static string GetCommentLink(Guid firstCommentId, TeamThread? teamThread, GameThread? gameThread)
        {
            return teamThread != null 
                ? $"{BaseRoute}/team-thread/{teamThread.Id}?firstComment={firstCommentId}"
                : $"{BaseRoute}/game-thread?homeTeam={gameThread!.HomeTeamApiId}&visitorTeam={gameThread!.VisitorTeamApiId}&date={gameThread!.Date}&firstComment={firstCommentId}";
        }

        public static string GetGameLink(int homeTeamApiId, int visitorTeamApiId, string date)
        {
            return $"{BaseRoute}/game?homeTeam={homeTeamApiId}&visitorTeam={visitorTeamApiId}&date={date}";
        }
    }
}