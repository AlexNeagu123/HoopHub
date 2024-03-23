using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.GetPlayerTeamHistory
{
    public class GetPlayerTeamHistoryQueryValidator : AbstractValidator<GetPlayerTeamHistoryQuery>
    {
        public GetPlayerTeamHistoryQueryValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage(ErrorMessages.PlayerIdEmpty);
        }
    }
}
