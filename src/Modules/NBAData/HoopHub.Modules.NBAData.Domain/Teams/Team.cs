using HoopHub.Modules.NBAData.Domain.Players;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoopHub.Modules.NBAData.Domain.Teams
{
    public class Team
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("api_id")]
        public int ApiId { get; private set; }

        [Column("full_name")]
        public string FullName { get; private set; }

        [Column("abbreviation")]
        public string Abbreviation { get; private set; }

        [Column("city")]
        public string City { get; private set; }

        [Column("conference")]
        public string Conference { get; private set; }

        [Column("division")]
        public string Division { get; private set; }

        [Column("image_url")]
        public string ImageUrl { get; private set; }
        public ICollection<Player> Players { get; private set; } 
    }
}
