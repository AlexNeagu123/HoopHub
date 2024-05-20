using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.CreatePlayerPerformanceReview
{
    public class CreatePlayerPerformanceReviewCommandValidator : AbstractValidator<CreatePlayerPerformanceReviewCommand>
    {
        public CreatePlayerPerformanceReviewCommandValidator(IPlayerPerformanceReviewRepository playerPerformanceReviewRepository, string fanId)
        {
            RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage(ValidationErrors.InvalidGameRating);
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage(ValidationErrors.InvalidPlayerId);
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var reviewResult = await playerPerformanceReviewRepository.FindByIdAsyncIncludingAll(command.HomeTeamId, command.VisitorTeamId, command.PlayerId, command.Date, fanId);
                return !reviewResult.IsSuccess;
            }).WithMessage(ValidationErrors.PlayerPerformanceReviewExists).WithName(ValidationKeys.PlayerPerformanceReview);
        }
    }
}
