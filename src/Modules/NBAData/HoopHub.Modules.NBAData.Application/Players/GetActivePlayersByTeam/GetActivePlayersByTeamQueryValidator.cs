using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Players.GetActivePlayersByTeam
{
    public class GetActivePlayersByTeamQueryValidator : AbstractValidator<GetActivePlayersByTeamQuery>
    {
        public GetActivePlayersByTeamQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage(ErrorMessages.TeamIdEmpty);
        }
    }
}
