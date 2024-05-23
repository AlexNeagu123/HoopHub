namespace HoopHub.Modules.NBAData.IntegrationEvents
{
    public record GameCreatedIntegrationEvent(
        Guid NotificationId,
        Guid GameId,
        DateTime Date,
        Guid HomeTeamId,
        Guid VisitorTeamId,
        Guid SeasonId,
        int HomeTeamScore,
        int VisitorTeamScore,
        string HomeTeamName,
        string VisitorTeamName,
        string? HomeTeamImageUrl,
        string? VisitorTeamImageUrl,
        int HomeTeamApiId,
        int VisitorTeamApiId,
        string Status,
        int? Period,
        string? Time,
        bool? Postseason);
}
