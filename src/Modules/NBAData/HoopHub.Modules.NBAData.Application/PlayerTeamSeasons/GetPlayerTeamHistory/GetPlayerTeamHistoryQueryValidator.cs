using FluentValidation;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.GetPlayerTeamHistory
{
    public class GetPlayerTeamHistoryQueryValidator : AbstractValidator<GetPlayerTeamHistoryQuery>
    {
        public GetPlayerTeamHistoryQueryValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage("PlayerId is required").WithName("PlayerId");
        }
    }
}
