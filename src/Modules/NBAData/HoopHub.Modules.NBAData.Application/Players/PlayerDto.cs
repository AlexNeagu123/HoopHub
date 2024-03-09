namespace HoopHub.Modules.NBAData.Application.Players
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public int ApiId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Position { get; set; }
        public string? Height { get; set; }
        public int? Weight { get; set; }
        public int? JerseyNumber { get; set; }
        public string? College { get; set; }
        public string? Country { get; set; }
        public int? DraftYear { get; set; }
        public int? DraftRound { get; set; }
        public int? DraftNumber { get; set; }
        public bool IsActive { get; private set; }
        public string? ImageUrl { get; set; }
    }
}
