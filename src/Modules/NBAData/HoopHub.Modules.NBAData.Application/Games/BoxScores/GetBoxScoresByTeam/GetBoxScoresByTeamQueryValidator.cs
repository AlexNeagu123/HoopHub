using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByTeam
{
    public class GetBoxScoresByTeamQueryValidator : AbstractValidator<GetBoxScoresByTeamQuery>
    {
        public GetBoxScoresByTeamQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage(ErrorMessages.TeamIdEmpty);
        }
    }
}
