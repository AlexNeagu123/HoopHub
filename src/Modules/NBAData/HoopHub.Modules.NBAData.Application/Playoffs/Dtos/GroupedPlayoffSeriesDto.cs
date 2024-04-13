namespace HoopHub.Modules.NBAData.Application.Playoffs.Dtos
{
    public class GroupedPlayoffSeriesDto
    {
        public Dictionary<string, List<PlayoffSeriesDto>> StageGrouping { get; set; }
    }
}
