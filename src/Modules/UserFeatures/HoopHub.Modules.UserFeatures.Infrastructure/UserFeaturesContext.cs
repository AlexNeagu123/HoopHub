using HoopHub.Modules.UserFeatures.Domain.Fans;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure
{
    public class UserFeaturesContext : DbContext
    {
        public DbSet<Fan> Fans { get; set; }

        public UserFeaturesContext(DbContextOptions<UserFeaturesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("user_features");
            modelBuilder.Entity<Fan>().ToTable("fans");
        }
    }
}
