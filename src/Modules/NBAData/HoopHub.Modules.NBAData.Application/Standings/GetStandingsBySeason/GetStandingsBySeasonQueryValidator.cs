using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Standings.GetStandingsBySeason
{
    public class GetStandingsBySeasonQueryValidator : AbstractValidator<GetStandingsBySeasonQuery>
    {
        public GetStandingsBySeasonQueryValidator()
        {
            RuleFor(x => x.Season).GreaterThanOrEqualTo(Config.MinStandingsSeason).LessThanOrEqualTo(Config.CurrentSeason).WithMessage(ErrorMessages.InvalidStandingsSeason);
        }
    }
}
