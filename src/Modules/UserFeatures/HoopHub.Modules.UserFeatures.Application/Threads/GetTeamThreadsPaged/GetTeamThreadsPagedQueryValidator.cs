using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadsPaged
{
    public class GetTeamThreadsPagedQueryValidator : AbstractValidator<GetTeamThreadsPagedQuery>
    {
        public GetTeamThreadsPagedQueryValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage(ValidationErrors.InvalidTeamId);
            RuleFor(x => x.Page).GreaterThan(0).WithMessage(ValidationErrors.InvalidPage);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(ValidationErrors.InvalidPageSize);
        }
    }
}
