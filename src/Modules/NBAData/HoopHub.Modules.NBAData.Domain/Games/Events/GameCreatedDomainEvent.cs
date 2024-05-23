using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.NBAData.Domain.Games.Events
{
    public class GameCreatedDomainEvent(DateTime date, Guid homeTeamId, Guid visitorTeamId, int homeTeamScore,
        int visitorTeamScore, string homeTeamName, string visitorTeamName, string? homeTeamImageUrl,
        string? visitorTeamImageUrl, int homeTeamApiId, int visitorTeamApiId, string status, Guid seasonId, 
        bool? postseason, string? time, int? period) : DomainEventBase
    {
        public DateTime Date { get; } = date;
        public Guid HomeTeamId { get; } = homeTeamId;
        public Guid VisitorTeamId { get; } = visitorTeamId;
        public int HomeTeamScore { get; } = homeTeamScore;
        public int VisitorTeamScore { get; } = visitorTeamScore;
        public string HomeTeamName { get; } = homeTeamName;
        public string VisitorTeamName { get; } = visitorTeamName;
        public string? HomeTeamImageUrl { get; } = homeTeamImageUrl;
        public string? VisitorTeamImageUrl { get; } = visitorTeamImageUrl;
        public int HomeTeamApiId { get; } = homeTeamApiId;
        public int VisitorTeamApiId { get; } = visitorTeamApiId;
        public string Status { get; } = status;
        public Guid SeasonId { get; } = seasonId;
        public bool? Postseason { get; } = postseason;
        public string? Time { get; } = time;
        public int? Period { get; } = period;
    }
}