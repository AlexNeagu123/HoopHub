using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReview
{
    public class GetGameReviewQueryValidator : AbstractValidator<GetGameReviewQuery>
    {
        public GetGameReviewQueryValidator()
        {
            RuleFor(x => x.HomeTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.VisitorTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
        }
    }
}
