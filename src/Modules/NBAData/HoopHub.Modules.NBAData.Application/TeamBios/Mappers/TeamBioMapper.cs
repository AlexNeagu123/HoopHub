using HoopHub.Modules.NBAData.Application.TeamBios.Dtos;
using HoopHub.Modules.NBAData.Domain.TeamBios;
using SeasonMapper = HoopHub.Modules.NBAData.Application.Seasons.Mappers.SeasonMapper;

namespace HoopHub.Modules.NBAData.Application.TeamBios.Mappers
{
    public class TeamBioMapper
    {
        private readonly SeasonMapper _seasonMapper = new();

        public TeamBioDto TeamBioToTeamBioDto(TeamBio teamBio)
        {

            return new TeamBioDto
            {
                Season = _seasonMapper.SeasonToSeasonDto(teamBio.Season),
                WinCount = teamBio.WinCount,
                LossCount = teamBio.LossCount,
                WinLossRatio = teamBio.WinLossRatio,
                Finish = teamBio.Finish,
                Srs = teamBio.Srs,
                Pace = teamBio.Pace,
                RelPace = teamBio.RelPace,
                ORtg = teamBio.ORtg,
                DRtg = teamBio.DRtg,
                Playoffs = teamBio.Playoffs
            };
        }
    }
}
