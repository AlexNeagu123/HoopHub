using HoopHub.Modules.NBAData.Domain.Players;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;
using HoopHub.Modules.NBAData.Domain.Seasons;
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

        public NBADataContext(DbContextOptions<NBADataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(p => p.Id);
            modelBuilder.Entity<Player>().ToTable("players");
            modelBuilder.Entity<Player>()
                .HasOne(p => p.CurrentTeam)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.CurrentTeamId);

            modelBuilder.Entity<Team>().HasKey(t => t.Id);
            modelBuilder.Entity<Team>().ToTable("teams");

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
            
            modelBuilder.Entity<Season>().HasKey(s => s.Id);
            modelBuilder.Entity<Season>().ToTable("seasons");

            modelBuilder.HasDefaultSchema("nba_data");
        }
    }
}
