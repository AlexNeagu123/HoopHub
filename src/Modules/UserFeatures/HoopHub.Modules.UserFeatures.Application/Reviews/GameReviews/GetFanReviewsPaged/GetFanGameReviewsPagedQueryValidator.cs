using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetFanReviewsPaged
{
    public class GetFanGameReviewsPagedQueryValidator : AbstractValidator<GetFanGameReviewsPagedQuery>
    {
        public GetFanGameReviewsPagedQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage(ValidationErrors.InvalidPage);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(ValidationErrors.InvalidPageSize);
        }
    }
}
