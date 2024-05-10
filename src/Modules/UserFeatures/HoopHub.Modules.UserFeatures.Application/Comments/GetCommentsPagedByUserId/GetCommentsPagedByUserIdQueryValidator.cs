using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetCommentsPagedByUserId
{
    public class GetCommentsPagedByUserIdQueryValidator : AbstractValidator<GetCommentsPagedByUserIdQuery>
    {
        public GetCommentsPagedByUserIdQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage(ValidationErrors.InvalidPage);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(ValidationErrors.InvalidPageSize);
        }
    }
}
