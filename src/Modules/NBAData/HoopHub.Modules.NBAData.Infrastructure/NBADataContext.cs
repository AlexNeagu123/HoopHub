using HoopHub.Modules.NBAData.Domain.Players;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.NBAData.Infrastructure
{
    public class NBADataContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public NBADataContext(DbContextOptions<NBADataContext> options) : base(options) { }
    }
}
