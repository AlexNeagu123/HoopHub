﻿using HoopHub.Modules.NBAData.Application.Seasons;
using HoopHub.Modules.NBAData.Domain.TeamBios;

namespace HoopHub.Modules.NBAData.Application.TeamBios
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
