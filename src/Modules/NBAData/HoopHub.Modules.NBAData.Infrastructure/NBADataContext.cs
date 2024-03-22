using HoopHub.Modules.NBAData.Domain.Players;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;
using HoopHub.Modules.NBAData.Domain.Seasons;
using HoopHub.Modules.NBAData.Domain.TeamBios;
using HoopHub.Modules.NBAData.Domain.Teams;
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
        public NBADataContext(DbContextOptions<NBADataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelPlayerTable(modelBuilder);
            ModelTeamTable(modelBuilder);
            ModelPlayerTeamSeasonTable(modelBuilder);
            ModelSeasonTable(modelBuilder);
            ModelTeamBioTable(modelBuilder);
           

            modelBuilder.HasDefaultSchema("nba_data");
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
