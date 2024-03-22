using FluentValidation;

namespace HoopHub.Modules.NBAData.Application.Players.GetBioByPlayerId
{
    public class GetBioByPlayerIdQueryValidator : AbstractValidator<GetBioByPlayerIdQuery>
    {
        public GetBioByPlayerIdQueryValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
}
