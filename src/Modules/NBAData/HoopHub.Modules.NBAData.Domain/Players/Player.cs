using HoopHub.Modules.NBAData.Domain.Teams;
using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;
using HoopHub.Modules.NBAData.Domain.AdvancedStatsEntries;

namespace HoopHub.Modules.NBAData.Domain.Players
{
    public class Player
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("api_id")]
        public int ApiId { get; private set; }

        [Column("first_name")]
        public string FirstName { get; private set; } = string.Empty;
        
        [Column("last_name")]
        public string LastName { get; private set; } = string.Empty;
        
        [Column("position")]
        public string? Position { get; private set; }
        
        [Column("height")]
        public string? Height { get; private set; }
        
        [Column("weight")]
        public string? Weight { get; private set; }
        
        [Column("jersey_number")]
        public string? JerseyNumber { get; private set; }
        
        [Column("college")]
        public string? College { get; private set; }
        
        [Column("country")]
        public string? Country { get; private set; }
        
        [Column("draft_year")]
        public int? DraftYear { get; private set; }
        
        [Column("draft_round")]
        public int? DraftRound { get; private set; }
        
        [Column("draft_number")]
        public int? DraftNumber { get; private set; }

        [Column("image_url")]
        public string? ImageUrl { get; private set; }

        [Column("is_active")]
        public bool IsActive { get; private set; }

        [Column("average_rating")]
        public decimal? AverageRating { get; private set; }

        [Column("current_team_id")]
        [ForeignKey("Team")]
        public Guid? CurrentTeamId { get; private set; }
        public Team? CurrentTeam { get; private set; }
        public ICollection<PlayerTeamSeason> PlayerTeamSeasons { get; private set; }
        public ICollection<BoxScores.BoxScores> BoxScores { get; private set; }
        public ICollection<AdvancedStatsEntry> AdvancedStatsEntries { get; private set; } = [];

        public void UpdateAverageRating(decimal? averageRating)
        {
            AverageRating = averageRating;
        }
    }
}
