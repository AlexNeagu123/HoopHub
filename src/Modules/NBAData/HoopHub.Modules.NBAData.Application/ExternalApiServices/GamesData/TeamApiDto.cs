namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData
{
    public class TeamApiDto
    {
        public int Id { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Abbreviation { get; set; }
    }
}
