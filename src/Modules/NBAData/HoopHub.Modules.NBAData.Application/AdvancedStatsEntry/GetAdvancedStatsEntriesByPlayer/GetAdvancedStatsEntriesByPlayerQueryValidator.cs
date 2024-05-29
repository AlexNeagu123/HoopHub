using FluentValidation;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.GetAdvancedStatsEntriesByPlayer
{
    public class GetAdvancedStatsEntriesByPlayerQueryValidator : AbstractValidator<GetAdvancedStatsEntriesByPlayerQuery>
    {
        public GetAdvancedStatsEntriesByPlayerQueryValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
}
