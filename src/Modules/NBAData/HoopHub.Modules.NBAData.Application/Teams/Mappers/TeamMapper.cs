using HoopHub.Modules.NBAData.Application.Constants;
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

        public TeamDto TeamToTeamDto(Team team, bool isLicensed)
        {
            var playerDtoList = team.Players is not null ? team.Players.Select(p => _playerMapper.PlayerToPlayerDto(p, isLicensed)).ToList() : [];
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
                ImageUrl = isLicensed ? team.ImageUrl : Config.DefaultTeamImageUrl,
                Players = playerDtoList,
                TeamBio = teamBio
            };
        }
    }
}
