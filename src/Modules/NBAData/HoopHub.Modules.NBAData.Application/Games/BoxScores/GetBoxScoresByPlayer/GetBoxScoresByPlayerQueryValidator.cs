using FluentValidation;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByPlayer
{
    public class GetBoxScoresByPlayerQueryValidator : AbstractValidator<GetBoxScoresByPlayerQuery>
    {
        public GetBoxScoresByPlayerQueryValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
}
