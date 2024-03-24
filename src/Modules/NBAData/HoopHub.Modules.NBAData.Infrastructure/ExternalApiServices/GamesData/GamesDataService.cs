using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Application.ExternalApiServices;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HoopHub.Modules.NBAData.Infrastructure.ExternalApiServices.GamesData
{
    public class GamesDataService(IConfiguration configuration) : BaseExternalApiService(configuration), IGamesDataService
    {
        public async Task<Result<IReadOnlyList<GameApiDto>>> GetGamesByDateAsync(string date)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BallDontLieBaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BallDontLieKey);

            try
            {
                var response = await client.GetAsync($"{Games}?dates[]={date}&per_page=100");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var games = JsonConvert.DeserializeObject<BaseExternalApiResponse<GameApiDto>>(responseBody);

                return games == null
                    ? Result<IReadOnlyList<GameApiDto>>.Failure("No data found")
                    : Result<IReadOnlyList<GameApiDto>>.Success(games.Data);
            }
            catch (Exception e)
            {
                return Result<IReadOnlyList<GameApiDto>>.Failure(e.Message);
            }
        }
    }
}
