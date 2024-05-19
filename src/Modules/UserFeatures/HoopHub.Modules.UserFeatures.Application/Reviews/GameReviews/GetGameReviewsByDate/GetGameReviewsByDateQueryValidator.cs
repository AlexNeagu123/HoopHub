using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReviewsByDate
{
    public class GetGameReviewsByDateQueryValidator : AbstractValidator<GetGameReviewsByDateQuery>
    {
        public GetGameReviewsByDateQueryValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage(ValidationErrors.InvalidDate);
        }
    }
}
