namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.SeasonAverageStats
{
    public class BaseExternalApiResponse<T> where T : class
    {
        public List<T> Data { get; set; } = [];
    }
}
