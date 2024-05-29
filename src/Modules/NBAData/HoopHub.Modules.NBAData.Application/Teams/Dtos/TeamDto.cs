using HoopHub.Modules.NBAData.Application.Players.Dtos;
using HoopHub.Modules.NBAData.Application.TeamBios.Dtos;

namespace HoopHub.Modules.NBAData.Application.Teams.Dtos
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public int ApiId { get; set; }
        public string? FullName { get; set; }
        public string? Abbreviation { get; set; }
        public string? City { get; set; }
        public string? Conference { get; set; }
        public string? Division { get; set; }
        public string? ImageUrl { get; set; }
        public List<PlayerDto> Players { get; set; } = [];
        public List<TeamBioDto> TeamBio { get; set; } = [];
    }
}
