namespace HoopHub.Modules.UserFeatures.Application.Threads.Dtos
{
    public class GameThreadDto
    {
        public Guid Id { get; set; }
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
        public int CommentsCount { get; set; }
    }
}
