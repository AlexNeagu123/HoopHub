using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Domain.Constants
{
    public class ClientRoutes
    {
        public static string GetCommentLink(Guid firstCommentId, TeamThread? teamThread, GameThread? gameThread, string? frontEndUrl)
        {
            return teamThread != null
                ? $"{frontEndUrl}/team-thread/{teamThread.Id}?firstComment={firstCommentId}"
                : $"{frontEndUrl}/game-thread?homeTeam={gameThread!.HomeTeamApiId}&visitorTeam={gameThread!.VisitorTeamApiId}&date={gameThread!.Date}&firstComment={firstCommentId}";
        }

        public static string GetGameLink(int homeTeamApiId, int visitorTeamApiId, string date, string? frontEndUrl)
        {
            return $"{frontEndUrl}/game?homeTeam={homeTeamApiId}&visitorTeam={visitorTeamApiId}&date={date}";
        }
    }
}