using HoopHub.Modules.UserAccess.Domain.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserAccess.Infrastructure
{
    public class UserAccessContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public UserAccessContext(DbContextOptions<UserAccessContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("user_access");
        }
    }
}
