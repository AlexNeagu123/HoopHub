﻿using HoopHub.Modules.NBAData.Domain.Players;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure
{
    public class NBADataContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public NBADataContext(DbContextOptions<NBADataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(p => p.Id);
            modelBuilder.HasDefaultSchema("nba-data");
            modelBuilder.Entity<Player>().HasData(SeedPlayers);
        }
        private static readonly Player[] SeedPlayers = new Player[]
        {
            Player.Create( "LeBron", "James").Value,
            Player.Create("Michael", "Jordan").Value,
        };
    }
}
