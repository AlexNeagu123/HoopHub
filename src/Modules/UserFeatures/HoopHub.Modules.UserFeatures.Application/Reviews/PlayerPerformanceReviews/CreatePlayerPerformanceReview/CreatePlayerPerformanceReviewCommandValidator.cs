﻿using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.CreatePlayerPerformanceReview
{
    public class CreatePlayerPerformanceReviewCommandValidator : AbstractValidator<CreatePlayerPerformanceReviewCommand>
    {
        public CreatePlayerPerformanceReviewCommandValidator(IPlayerPerformanceReviewRepository playerPerformanceReviewRepository, string fanId)
        {
            RuleFor(x => x.HomeTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.Rating).InclusiveBetween(1, 10).WithMessage(ValidationErrors.InvalidGameRating);
            RuleFor(x => x.VisitorTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage(ValidationErrors.InvalidPlayerId);
            RuleFor(x => x.Date).Must(BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var reviewResult = await playerPerformanceReviewRepository.FindByIdAsyncIncludingAll(command.HomeTeamId, command.VisitorTeamId, command.PlayerId, command.Date, fanId);
                return !reviewResult.IsSuccess;
            }).WithMessage(ValidationErrors.PlayerPerformanceReviewExists).WithName(ValidationKeys.PlayerPerformanceReview);
        }
        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, Config.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
