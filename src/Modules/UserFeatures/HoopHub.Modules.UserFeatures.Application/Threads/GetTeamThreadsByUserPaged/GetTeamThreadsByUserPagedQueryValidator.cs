using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadsByUserPaged
{
    public class GetTeamThreadsByUserPagedQueryValidator : AbstractValidator<GetTeamThreadsByUserPagedQuery>
    {
        public GetTeamThreadsByUserPagedQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage(ValidationErrors.InvalidPage);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(ValidationErrors.InvalidPageSize);
        }
    }
}
