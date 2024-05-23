using System.ComponentModel.DataAnnotations.Schema;

namespace HoopHub.Modules.NBAData.Domain.OutboxMessages
{
    public class NBADataOutboxMessage
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("type")]
        public string Type { get; set; } = string.Empty;

        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("occured_on_utc")]
        public DateTime OccuredOnUtc { get; set; }

        [Column("processed_on_utc")]
        public DateTime? ProcessedOnUtc { get; set; }

        [Column("error")]
        public string? Error { get; set; }
    }
}
