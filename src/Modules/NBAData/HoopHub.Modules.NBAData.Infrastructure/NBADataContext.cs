using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.AdvancedStatsEntries;
using HoopHub.Modules.NBAData.Domain.BoxScores;
using HoopHub.Modules.NBAData.Domain.Games;
using HoopHub.Modules.NBAData.Domain.OutboxMessages;
using HoopHub.Modules.NBAData.Domain.Players;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;
using HoopHub.Modules.NBAData.Domain.Seasons;
using HoopHub.Modules.NBAData.Domain.Standings;
using HoopHub.Modules.NBAData.Domain.TeamBios;
using HoopHub.Modules.NBAData.Domain.Teams;
using HoopHub.Modules.NBAData.Domain.TeamsLatest;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure
{
    public class NBADataContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PlayerTeamSeason> PlayerTeamSeasons { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<TeamBio> TeamBios { get; set; }
        public DbSet<PlayoffSeries> PlayoffSeries { get; set; }
        public DbSet<TeamLatest> TeamsLatest { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<AdvancedStatsEntry> AdvancedStatsEntries { get; set; }
        public DbSet<BoxScores> BoxScores { get; set; }
        public DbSet<NBADataOutboxMessage> OutboxMessages { get; set; }
        public NBADataContext(DbContextOptions<NBADataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelPlayerTable(modelBuilder);
            ModelTeamTable(modelBuilder);
            ModelPlayerTeamSeasonTable(modelBuilder);
            ModelSeasonTable(modelBuilder);
            ModelTeamBioTable(modelBuilder);
            ModelStandingsTable(modelBuilder);
            ModelPlayoffSeriesTable(modelBuilder);
            ModelTeamLatestTable(modelBuilder);
            ModelGamesTable(modelBuilder);
            ModelBoxScoresTable(modelBuilder);
            ModelAdvancedStatsTable(modelBuilder);
            ModelOutboxMessagesTable(modelBuilder);
            modelBuilder.HasDefaultSchema("nba_data");
        }

        private static void ModelAdvancedStatsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvancedStatsEntry>().HasKey(a => a.Id);

            modelBuilder.Entity<AdvancedStatsEntry>()
                .HasOne(a => a.Game)
                .WithMany(g => g.AdvancedStatsEntries)
                .HasForeignKey(a => a.GameId);

            modelBuilder.Entity<AdvancedStatsEntry>()
                .HasOne(a => a.Player)
                .WithMany(p => p.AdvancedStatsEntries)
                .HasForeignKey(a => a.PlayerId);

            modelBuilder.Entity<AdvancedStatsEntry>()
                .HasOne(a => a.Team)
                .WithMany(t => t.AdvancedStatsEntries)
                .HasForeignKey(a => a.TeamId);

            modelBuilder.Entity<AdvancedStatsEntry>().ToTable("advanced_stats");
        }

        private static void ModelOutboxMessagesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NBADataOutboxMessage>().HasKey(om => om.Id);
            modelBuilder.Entity<NBADataOutboxMessage>().ToTable("outbox_messages");
        }

        private static void ModelBoxScoresTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoxScores>().HasKey(bs => bs.Id);
            modelBuilder.Entity<BoxScores>()
                .HasOne(bs => bs.Game)
                .WithMany(g => g.BoxScores)
                .HasForeignKey(bs => bs.GameId);

            modelBuilder.Entity<BoxScores>()
                .HasOne(bs => bs.Player)
                .WithMany(p => p.BoxScores)
                .HasForeignKey(bs => bs.PlayerId);

            modelBuilder.Entity<BoxScores>()
                .HasOne(bs => bs.Team)
                .WithMany(t => t.BoxScores)
                .HasForeignKey(bs => bs.TeamId);

            modelBuilder.Entity<BoxScores>().ToTable("box_scores");
        }

        private static void ModelGamesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasKey(g => g.Id);
            modelBuilder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.VisitorTeam)
                .WithMany(t => t.VisitorGames)
                .HasForeignKey(g => g.VisitorTeamId);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Season)
                .WithMany(s => s.Games)
                .HasForeignKey(g => g.SeasonId);

            modelBuilder.Entity<Game>().ToTable("games");
        }

        private static void ModelTeamLatestTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamLatest>().HasKey(tl => new { tl.TeamId, tl.LatestIndex });
            modelBuilder.Entity<TeamLatest>()
                .HasOne(tl => tl.Team)
                .WithMany(t => t.TeamLatest)
                .HasForeignKey(tl => tl.TeamId);

            modelBuilder.Entity<TeamLatest>().ToTable("team_latest");
        }

        private static void ModelPlayoffSeriesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayoffSeries>().HasKey(ps => new { ps.SeasonId, ps.WinningTeamId, ps.LosingTeamId });
            modelBuilder.Entity<PlayoffSeries>()
                .HasOne(ps => ps.Season)
                .WithMany(s => s.PlayoffSeries)
                .HasForeignKey(ps => ps.SeasonId);

            modelBuilder.Entity<PlayoffSeries>()
                .HasOne(ps => ps.WinningTeam)
                .WithMany(t => t.PlayoffSeries)
                .HasForeignKey(ps => ps.WinningTeamId);


            modelBuilder.Entity<PlayoffSeries>().ToTable("playoff_series");
        }
        private static void ModelStandingsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StandingsEntry>().HasKey(se => new { se.SeasonId, se.TeamId });
            modelBuilder.Entity<StandingsEntry>()
                .HasOne(se => se.Season)
                .WithMany(s => s.Standings)
                .HasForeignKey(se => se.SeasonId);

            modelBuilder.Entity<StandingsEntry>()
                .HasOne(se => se.Team)
                .WithMany(t => t.Standings)
                .HasForeignKey(se => se.TeamId);

            modelBuilder.Entity<StandingsEntry>().ToTable("standings");
        }
        private static void ModelTeamBioTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamBio>().HasKey(tb => tb.Id);
            modelBuilder.Entity<TeamBio>()
                .HasOne(tb => tb.Team)
                .WithMany(t => t.TeamBio)
                .HasForeignKey(tb => tb.TeamId);

            modelBuilder.Entity<TeamBio>()
                .HasOne(tb => tb.Season)
                .WithMany(s => s.TeamBio)
                .HasForeignKey(tb => tb.SeasonId);

            modelBuilder.Entity<TeamBio>().ToTable("teams_bio");
        }

        private static void ModelSeasonTable(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Season>().HasKey(s => s.Id);
            modelBuilder.Entity<Season>().ToTable("seasons");
        }

        private static void ModelPlayerTeamSeasonTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerTeamSeason>()
                .HasKey(pt => new { pt.PlayerId, pt.TeamId, pt.SeasonId });

            modelBuilder.Entity<PlayerTeamSeason>()
                .HasOne(pt => pt.Player)
                .WithMany(p => p.PlayerTeamSeasons)
                .HasForeignKey(pt => pt.PlayerId);

            modelBuilder.Entity<PlayerTeamSeason>()
                .HasOne(pt => pt.Team)
                .WithMany(t => t.PlayerTeamSeasons)
                .HasForeignKey(pt => pt.TeamId);

            modelBuilder.Entity<PlayerTeamSeason>()
                .HasOne(pt => pt.Season)
                .WithMany(s => s.PlayerTeamSeasons)
                .HasForeignKey(pt => pt.SeasonId);

            modelBuilder.Entity<PlayerTeamSeason>().ToTable("player_team_season");
        }

        private static void ModelTeamTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasKey(t => t.Id);
            modelBuilder.Entity<Team>().ToTable("teams");
        }

        private static void ModelPlayerTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(p => p.Id);
            modelBuilder.Entity<Player>().ToTable("players");
            modelBuilder.Entity<Player>()
                .HasOne(p => p.CurrentTeam)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.CurrentTeamId);
        }
    }
}
