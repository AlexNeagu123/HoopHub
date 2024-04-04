using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Games.GetBoxScoreByGame
{
    public class GetBoxScoreByGameQueryValidator : AbstractValidator<GetBoxScoreByGameQuery>
    {
        public GetBoxScoreByGameQueryValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(ErrorMessages.DateEmpty)
                .Must(BeAValidDate).WithMessage(ErrorMessages.DateInvalidFormat);
            RuleFor(x => x.HomeTeamApiId).NotEmpty().WithMessage(ErrorMessages.HomeTeamApiIdEmpty);
            RuleFor(x => x.VisitorTeamApiId).NotEmpty().WithMessage(ErrorMessages.VisitorTeamApiIdEmpty);
        }
        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, Config.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
