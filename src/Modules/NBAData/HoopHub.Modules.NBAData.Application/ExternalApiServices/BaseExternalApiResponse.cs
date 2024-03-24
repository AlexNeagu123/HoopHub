namespace HoopHub.Modules.NBAData.Application.ExternalApiServices
{
    public class BaseExternalApiResponse<T> where T : class
    {
        public List<T> Data { get; set; } = [];
        public MetaDto? Meta { get; set; }
    }
}
