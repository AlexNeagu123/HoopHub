using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Application.Games.Mappers
{
    public class BoxScoreTeamMapper
    {
        public BoxScoreTeamDto TeamToBoxScoreTeamDto(Team team, bool isLicensed)
        {
            return new BoxScoreTeamDto
            {
                Id = team.Id,
                ApiId = team.ApiId,
                FullName = team.FullName,
                Abbreviation = team.Abbreviation,
                City = team.City,
                Conference = team.Conference,
                Division = team.Division,
                ImageUrl = isLicensed ? team.ImageUrl : Config.DefaultTeamImageUrl,
                Players = []
            };
        }
    }
}
