using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByTeam;

namespace HoopHub.Modules.NBAData.Application.Games.GetGamesByTeam
{
    public class GetBoxScoresByTeamQueryValidator : AbstractValidator<GetBoxScoresByTeamQuery>
    {
        public GetBoxScoresByTeamQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage(ErrorMessages.TeamIdEmpty);
        }
    }
}
