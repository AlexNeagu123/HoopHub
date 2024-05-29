using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Application.ExternalApiServices;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.AdvancedStatsData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HoopHub.Modules.NBAData.Infrastructure.ExternalApiServices.AdvancedStatsData
{
    public class AdvancedStatsDataService(IConfiguration configuration) : BaseExternalApiService(configuration), IAdvancedStatsDataService
    {
        public async Task<Result<IReadOnlyList<AdvancedStatsApiDto>>> GetAdvancedStatsAsyncByDate(string date)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BallDontLieBaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BallDontLieKey);

            var allStats = new List<AdvancedStatsApiDto>();
            int? nextCursor = null;

            try
            {
                do
                {
                    var cursorQuery = nextCursor.HasValue ? $"&cursor={nextCursor.Value}" : string.Empty;
                    var response = await client.GetAsync($"stats/advanced?dates[]={date}&per_page=100{cursorQuery}");
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var advancedStats = JsonConvert.DeserializeObject<BaseExternalApiResponse<AdvancedStatsApiDto>>(responseBody);

                    if (advancedStats?.Data == null)
                        break;


                    allStats.AddRange(advancedStats.Data);

                    if (advancedStats.Meta == null)
                        break;

                    nextCursor = advancedStats.Meta.NextCursor;

                } while (nextCursor.HasValue);

                return Result<IReadOnlyList<AdvancedStatsApiDto>>.Success(allStats);
            }
            catch (Exception e)
            {
                return Result<IReadOnlyList<AdvancedStatsApiDto>>.Failure(e.Message);
            }
        }
    }
}
