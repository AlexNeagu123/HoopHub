using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Teams.GetBioByTeamId
{
    public class GetBioByTeamIdQueryValidator : AbstractValidator<GetBioByTeamIdQuery>
    {
        public GetBioByTeamIdQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage(ErrorMessages.TeamIdEmpty);
        }
    }
}
