﻿using HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;
using SeasonMapper = HoopHub.Modules.NBAData.Application.Seasons.Mappers.SeasonMapper;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.Mappers
{
    public class PlayerTeamSeasonMapper
    {
        private readonly TeamMapper _teamMapper = new();
        private readonly SeasonMapper _seasonMapper = new();

        public PlayerTeamSeasonDto PlayerTeamSeasonToPlayerTeamSeasonDto(PlayerTeamSeason playerTeamSeason, bool isLicensed)
        {
            return new PlayerTeamSeasonDto
            {
                Team = _teamMapper.TeamToTeamDto(playerTeamSeason.Team, isLicensed),
                Season = _seasonMapper.SeasonToSeasonDto(playerTeamSeason.Season)
            };
        }
    }
}
