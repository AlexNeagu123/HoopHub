using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.NBAData.Domain.BoxScores.Events
{
    public class BoxScoresCreatedDomainEvent(
        Guid gameId,
        Guid playerId,
        Guid teamId,
        int homeTeamApiId,
        int visitorTeamApiId,
        string playerName,
        DateTime date,
        string? playerImageUrl,
        string? min,
        int? fgm,
        int? fga,
        double? fgPct,
        int? fg3m,
        int? fg3a,
        double? fg3Pct,
        int? ftm,
        int? fta,
        double? ftPct,
        int? oreb,
        int? dreb,
        int? reb,
        int? ast,
        int? stl,
        int? blk,
        int? turnover,
        int? pf,
        int? pts) : DomainEventBase
    {
        public Guid GameId { get; } = gameId;
        public Guid PlayerId { get; } = playerId;
        public Guid TeamId { get; } = teamId;
        public int HomeTeamApiId { get; } = homeTeamApiId;
        public int VisitorTeamApiId { get; } = visitorTeamApiId;
        public string PlayerName { get; } = playerName;
        public string? PlayerImageUrl { get; } = playerImageUrl;
        public DateTime Date { get; } = date;
        public string? Min { get; } = min;
        public int? Fgm { get; } = fgm;
        public int? Fga { get; } = fga;
        public double? FgPct { get; } = fgPct;
        public int? Fg3m { get; } = fg3m;
        public int? Fg3a { get; } = fg3a;
        public double? Fg3Pct { get; } = fg3Pct;
        public int? Ftm { get; } = ftm;
        public int? Fta { get; } = fta;
        public double? FtPct { get; } = ftPct;
        public int? Oreb { get; } = oreb;
        public int? Dreb { get; } = dreb;
        public int? Reb { get; } = reb;
        public int? Ast { get; } = ast;
        public int? Stl { get; } = stl;
        public int? Blk { get; } = blk;
        public int? Turnover { get; } = turnover;
        public int? Pf { get; } = pf;
        public int? Pts { get; } = pts;
    }
}