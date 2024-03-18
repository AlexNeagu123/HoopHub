using FluentValidation;

namespace HoopHub.Modules.NBAData.Application.Teams.GetTeamById
{
    public class GetTeamByIdQueryValidator : AbstractValidator<GetTeamByIdQuery>
    {
        public GetTeamByIdQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().NotNull().WithMessage("Id is required").WithName("Id");
        }
    }
}
