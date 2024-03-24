using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Games.GetAllGamesByDate
{
    public class GetAllGamesByDateQueryValidator : AbstractValidator<GetAllGamesByDateQuery>
    {
        public GetAllGamesByDateQueryValidator() 
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(ErrorMessages.DateEmpty)
                .Must(BeAValidDate).WithMessage(ErrorMessages.DateInvalidFormat);
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, Config.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
