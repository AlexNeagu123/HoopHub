using HoopHub.Modules.NBAData.Application.Players;

namespace HoopHub.Modules.NBAData.Application.Teams
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public int ApiId { get; set; }
        public string FullName { get; set; }
        public string Abbreviation { get; set; }
        public string City { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public string ImageUrl { get; set; }
        public IReadOnlyList<PlayerDto>? Players { get; set; } = null;
    }
}
