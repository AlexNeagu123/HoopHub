using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Games.GetGamesByTeam
{
    public class GetBoxScoresByTeamQueryValidator : AbstractValidator<GetBoxScoresByTeamQuery>
    {
        public GetBoxScoresByTeamQueryValidator()
        {
            RuleFor(x => x.ApiId).NotEmpty().WithMessage(ErrorMessages.TeamIdEmpty);
            RuleFor(x => x.GameCount).GreaterThan(0).WithMessage(ErrorMessages.GameCountInvalid);
        }
    }
}
