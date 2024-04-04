using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Application.ExternalApiServices;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HoopHub.Modules.NBAData.Infrastructure.ExternalApiServices.BoxScoresData
{
    public class BoxScoresDataService(IConfiguration configuration) : BaseExternalApiService(configuration), IBoxScoresDataService
    {
        public async Task<Result<IReadOnlyList<BoxScoreApiDto>>> GetBoxScoresAsyncByDate(string date)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BallDontLieBaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BallDontLieKey);

            try
            {
                var response = await client.GetAsync($"{BoxScores}?date={date}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var boxScores = JsonConvert.DeserializeObject<BaseExternalApiResponse<BoxScoreApiDto>>(responseBody);

                return boxScores == null
                    ? Result<IReadOnlyList<BoxScoreApiDto>>.Failure("No data found")
                    : Result<IReadOnlyList<BoxScoreApiDto>>.Success(boxScores.Data);
            }
            catch (Exception e)
            {
                return Result<IReadOnlyList<BoxScoreApiDto>>.Failure(e.Message);
            }
        }

        public async Task<Result<BoxScoreApiDto>> GetBoxScoresAsyncByTeamAndDate(string date, int teamId)
        {
            var boxScores = await GetBoxScoresAsyncByDate(date);
            foreach (var boxScore in boxScores.Value)
            {
                if (boxScore.HomeTeam?.Id == teamId || boxScore.VisitorTeam?.Id == teamId)
                {
                    return Result<BoxScoreApiDto>.Success(boxScore);
                }
            }
            return Result<BoxScoreApiDto>.Failure("No data found");
        }
    }
}
