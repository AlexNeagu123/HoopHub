using FluentValidation;

namespace HoopHub.Modules.NBAData.Application.Teams.GetTeamById
{
    public class GetTeamByIdQueryValidator : AbstractValidator<GetTeamByIdQuery>
    {
        public GetTeamByIdQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().NotNull().WithMessage("PlayerId is required").WithName("PlayerId");
        }
    }
}
