using HoopHub.Modules.UserAccess.Infrastructure;
using HoopHub.Modules.UserFeatures.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            
            var userAccessContext= scope.ServiceProvider.GetRequiredService<UserAccessContext>();
            userAccessContext.Database.EnsureCreated();
            userAccessContext.Database.Migrate();

            var userFeaturesContext = scope.ServiceProvider.GetRequiredService<UserFeaturesContext>();
            userFeaturesContext.Database.EnsureCreated();
            userFeaturesContext.Database.Migrate();
        }
    }
}
