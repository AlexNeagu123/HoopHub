using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.UpdateGameReview
{
    public class UpdateGameReviewCommandValidator : AbstractValidator<UpdateGameReviewCommand>
    {
        public UpdateGameReviewCommandValidator(IGameReviewRepository gameReviewRepository, string fanId)
        {
            RuleFor(x => x.HomeTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.Rating).InclusiveBetween(0, 5).WithMessage(ValidationErrors.InvalidGameRating);
            RuleFor(x => x.VisitorTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var gameReviewResult = await gameReviewRepository.FindByIdAsyncIncludingAll(command.HomeTeamId, command.VisitorTeamId, command.Date, fanId);
                return gameReviewResult.IsSuccess && fanId == gameReviewResult.Value.FanId;
            }).WithMessage(ValidationErrors.GameReviewAlreadyExists).WithName(ValidationKeys.GameReview);
        }
    }
}
