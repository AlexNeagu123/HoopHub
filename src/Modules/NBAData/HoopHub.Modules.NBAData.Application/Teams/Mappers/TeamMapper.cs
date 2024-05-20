using HoopHub.Modules.NBAData.Application.Players;
using HoopHub.Modules.NBAData.Application.TeamBios.Mappers;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;
using HoopHub.Modules.NBAData.Domain.Teams;
using PlayerMapper = HoopHub.Modules.NBAData.Application.Players.Mappers.PlayerMapper;


namespace HoopHub.Modules.NBAData.Application.Teams.Mappers
{

    public class TeamMapper
    {
        private readonly PlayerMapper _playerMapper = new();
        private readonly TeamBioMapper _teamBioMapper = new();

        public TeamDto TeamToTeamDto(Team team)
        {
            var playerDtoList = team.Players is not null ? team.Players.Select(_playerMapper.PlayerToPlayerDto).ToList() : [];
            var teamBio = team.TeamBio is not null ? team.TeamBio.Select(_teamBioMapper.TeamBioToTeamBioDto).ToList() : [];

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
                Players = playerDtoList,
                TeamBio = teamBio
            };
        }
    }
}
