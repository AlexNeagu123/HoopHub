﻿using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.DeletePlayerPerformanceReview
{
    public class DeletePlayerPerformanceReviewCommandValidator : AbstractValidator<DeletePlayerPerformanceReviewCommand>
    {
        public DeletePlayerPerformanceReviewCommandValidator(IPlayerPerformanceReviewRepository playerPerformanceReviewRepository, string fanId)
        {
            RuleFor(x => x.PlayerId).NotEmpty().WithMessage(ValidationErrors.InvalidPlayerId);
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var playerPerformanceReviewResult = await playerPerformanceReviewRepository.FindByIdAsyncIncludingAll(command.HomeTeamId, command.VisitorTeamId, command.PlayerId, command.Date, fanId);
                return playerPerformanceReviewResult.IsSuccess && fanId == playerPerformanceReviewResult.Value.FanId;
            }).WithMessage(ValidationErrors.PlayerPerformanceReviewDoNotExist).WithName(ValidationKeys.PlayerPerformanceReview);
        }
    }
}
