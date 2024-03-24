using HoopHub.Modules.NBAData.Application.Players.Dtos;

namespace HoopHub.Modules.NBAData.Application.Games.Dtos
{
    public class BoxScoreTeamDto
    {
        public Guid Id { get; set; }
        public int ApiId { get; set; }
        public string FullName { get; set; }
        public string Abbreviation { get; set; }
        public string City { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public string ImageUrl { get; set; }
        public List<BoxScorePlayerDto> Players { get; set; } = [];
    }
}
