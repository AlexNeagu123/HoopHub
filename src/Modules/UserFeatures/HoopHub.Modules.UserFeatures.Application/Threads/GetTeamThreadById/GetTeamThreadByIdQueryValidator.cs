using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadById
{
    public class GetTeamThreadByIdQueryValidator : AbstractValidator<GetTeamThreadByIdQuery>
    {
        public GetTeamThreadByIdQueryValidator()
        {
            RuleFor(x => x.TeamThreadId).NotEmpty().WithMessage(ValidationErrors.InvalidThreadId);
        }
    }
}
