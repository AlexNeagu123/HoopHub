using HoopHub.Modules.NBAData.Infrastructure;

namespace HoopHub.API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NBADataContext>();
            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();
        }
    }
}
