using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Playoffs.GetPlayoffSeriesBySeason
{
    public class GetPlayoffSeriesBySeasonQueryValidator : AbstractValidator<GetPlayoffSeriesBySeasonQuery>
    {
        public GetPlayoffSeriesBySeasonQueryValidator()
        {
            RuleFor(x => x.Season).GreaterThanOrEqualTo(Config.MinStandingsSeason).LessThanOrEqualTo(Config.CurrentSeason).WithMessage(ErrorMessages.InvalidStandingsSeason);
        }
    }
}
