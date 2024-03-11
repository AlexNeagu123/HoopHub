using HoopHub.Modules.UserAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UserAccessContext>();
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
        }
    }
}
