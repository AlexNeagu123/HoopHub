using System.Net.Http.Headers;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.SeasonAverageStats;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Infrastructure.ExternalApiServices
{
    public class SeasonAverageStatsService(IConfiguration configuration) : BaseExternalApiService(configuration), ISeasonAverageStatsService
    {
        public async Task<Result<SeasonAverageStatsDto>> GetAverageStatsBySeasonIdAndPlayerIdAsync(string season, int playerApiId)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BallDontLieBaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BallDontLieKey);

            try
            {
                var index = season.IndexOf('-');
                if (index == -1)
                    throw new ArgumentException("Invalid season format. Should be in the format 'YYYY-YY'");

                season = season[..index];

                var response =
                    await client.GetAsync($"{SeasonAverages}?season={season}&player_ids[]={playerApiId}");

                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var seasonAverageStats = JsonConvert.DeserializeObject<BaseExternalApiResponse<SeasonAverageStatsDto>>(responseBody);

                return seasonAverageStats == null 
                    ? Result<SeasonAverageStatsDto>.Failure("No data found") 
                    : Result<SeasonAverageStatsDto>.Success(seasonAverageStats.Data[0]);
            }
            catch (Exception e)
            {
                return Result<SeasonAverageStatsDto>.Failure(e.Message);
            }
        }
    }
}
