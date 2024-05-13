namespace HoopHub.Modules.UserFeatures.Application.Threads.Dtos
{
    public class GameThreadDto
    {
        public Guid Id { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
        public int CommentsCount { get; set; }
    }
}
