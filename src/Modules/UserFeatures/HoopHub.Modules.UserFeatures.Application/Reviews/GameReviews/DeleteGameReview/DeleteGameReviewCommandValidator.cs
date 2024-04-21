using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.DeleteGameReview
{
    public class DeleteGameReviewCommandValidator : AbstractValidator<DeleteGameReviewCommand>
    {
        public DeleteGameReviewCommandValidator(IGameReviewRepository gameReviewRepository, string fanId)
        {
            RuleFor(x => x.HomeTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.VisitorTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.Date).Must(BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var gameReviewResult = await gameReviewRepository.FindByIdAsyncIncludingAll(command.HomeTeamId, command.VisitorTeamId, command.Date, fanId);
                return gameReviewResult.IsSuccess && gameReviewResult.Value.FanId == fanId;
            }).WithMessage(ValidationErrors.GameReviewDoNotExist).WithName(ValidationKeys.GameReview);
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, Config.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
