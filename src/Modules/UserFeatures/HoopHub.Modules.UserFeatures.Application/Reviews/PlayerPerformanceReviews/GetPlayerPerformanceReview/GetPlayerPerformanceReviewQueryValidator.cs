using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.GetPlayerPerformanceReview
{
    public class GetPlayerPerformanceReviewQueryValidator : AbstractValidator<GetPlayerPerformanceReviewQuery>
    {
        public GetPlayerPerformanceReviewQueryValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage(ValidationErrors.InvalidPlayerId);
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
        }
    }
}
