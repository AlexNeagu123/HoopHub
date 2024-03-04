using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HoopHub.Modules.NBAData.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<NBADataContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("HoopHubConnection"),
                    builder => builder.MigrationsAssembly(typeof(NBADataContext).Assembly.FullName))
            );
            return services;
        }
    }
}
