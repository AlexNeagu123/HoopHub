using FluentValidation;
using HoopHub.Modules.NBAData.Application.Constants;

namespace HoopHub.Modules.NBAData.Application.Teams.GetTeamById
{
    public class GetTeamByIdQueryValidator : AbstractValidator<GetTeamByIdQuery>
    {
        public GetTeamByIdQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().NotNull().WithMessage(ErrorMessages.TeamIdEmpty);
        }
    }
}
