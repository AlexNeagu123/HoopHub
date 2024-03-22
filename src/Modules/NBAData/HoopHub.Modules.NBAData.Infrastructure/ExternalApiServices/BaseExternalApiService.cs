using Microsoft.Extensions.Configuration;

namespace HoopHub.Modules.NBAData.Infrastructure.ExternalApiServices
{
    public class BaseExternalApiService(IConfiguration configuration)
    {
        public const string BallDontLieBaseUrl = "https://api.balldontlie.io/v1/";
        public const string SeasonAverages = "season_averages";
        public string BallDontLieKey { get; } = configuration["External:BallDontLieKey"]!;
    }
}
