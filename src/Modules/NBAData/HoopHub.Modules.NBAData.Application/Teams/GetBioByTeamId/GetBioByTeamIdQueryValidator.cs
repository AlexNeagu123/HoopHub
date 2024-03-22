using FluentValidation;

namespace HoopHub.Modules.NBAData.Application.Teams.GetBioByTeamId
{
    public class GetBioByTeamIdQueryValidator : AbstractValidator<GetBioByTeamIdQuery>
    {
        public GetBioByTeamIdQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty();
        }
    }
}
