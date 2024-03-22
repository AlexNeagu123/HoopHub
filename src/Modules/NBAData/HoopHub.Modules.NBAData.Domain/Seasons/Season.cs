﻿using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;
using HoopHub.Modules.NBAData.Domain.TeamBios;

namespace HoopHub.Modules.NBAData.Domain.Seasons
{
    public class Season
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("season")]
        public string SeasonPeriod { get; private set; } = string.Empty;

        public ICollection<PlayerTeamSeason> PlayerTeamSeasons { get; private set; }
        public ICollection<TeamBio> TeamBio { get; private set; }
    }
}
