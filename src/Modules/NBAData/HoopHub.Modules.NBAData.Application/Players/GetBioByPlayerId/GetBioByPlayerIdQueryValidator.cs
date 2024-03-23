using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Players.GetBioByPlayerId
{
    public class GetBioByPlayerIdQueryValidator : AbstractValidator<GetBioByPlayerIdQuery>
    {
        public GetBioByPlayerIdQueryValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage(ErrorMessages.PlayerIdEmpty);
            RuleFor(x => x.EndSeason).GreaterThan(x => x.StartSeason).WithMessage(ErrorMessages.StartSeasonGreaterThanEndSeason);
        }
    }
}
