using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.GetPlayerPerformanceReviewsByGame
{
    public class GetPlayerPerformanceReviewsByGameQueryValidator : AbstractValidator<GetPlayerPerformanceReviewsByGameQuery>
    {
        public GetPlayerPerformanceReviewsByGameQueryValidator()
        {
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
        }
    }
}
