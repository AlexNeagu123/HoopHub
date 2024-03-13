using FluentValidation;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.GetPlayerTeamHistory
{
    public class GetPlayerTeamHistoryQueryValidator : AbstractValidator<GetPlayerTeamHistoryQuery>
    {
        public GetPlayerTeamHistoryQueryValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage("Id is required").WithName("Id");
        }
    }
}
