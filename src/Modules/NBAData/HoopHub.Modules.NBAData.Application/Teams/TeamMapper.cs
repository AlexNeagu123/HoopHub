using HoopHub.Modules.NBAData.Application.Players;
using HoopHub.Modules.NBAData.Application.Seasons;
using HoopHub.Modules.NBAData.Domain.Teams;


namespace HoopHub.Modules.NBAData.Application.Teams {

    public class TeamMapper
    {
        private readonly PlayerMapper _playerMapper = new();

        public TeamDto TeamToTeamDto(Team team)
        {
            var playerDtoList = team.Players is not null ?
                team.Players.Select(_playerMapper.PlayerToPlayerDto).ToList() : null;
            
            return new TeamDto
            {
                Id = team.Id,
                ApiId = team.ApiId,
                FullName = team.FullName,
                Abbreviation = team.Abbreviation,
                City = team.City,
                Conference = team.Conference,
                Division = team.Division,
                ImageUrl = team.ImageUrl,
                Players = playerDtoList
            };
        }
    }
}
