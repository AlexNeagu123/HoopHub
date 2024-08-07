﻿using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadComment
{
    public class CreateThreadCommentCommandValidator : AbstractValidator<CreateThreadCommentCommand>
    {
        public CreateThreadCommentCommandValidator(ITeamThreadRepository teamThreadRepository, IGameThreadRepository gameThreadRepository)
        {
            RuleFor(x => x.Content).NotEmpty().Length(Config.ContentMinLength, Config.ContentMaxLength).WithMessage(ValidationErrors.InvalidCommentContent);
            RuleFor(x => x.TeamThreadId).Must((command, teamThreadId) => (teamThreadId == null) ^ (command.GameThreadId == null)).WithMessage(ValidationErrors.ShouldBeExactlyOneThreadNonNull);
            RuleFor(x => x.TeamThreadId).MustAsync(async (teamThreadId, cancellation) =>
            {
                if (teamThreadId == null)
                    return true;
                var teamThreadResult = await teamThreadRepository.FindByIdAsync(teamThreadId.Value);
                return teamThreadResult.IsSuccess;
            }).WithMessage(ValidationErrors.ThreadDoNotExist).WithName(ValidationKeys.TeamThread);

            RuleFor(x => x.GameThreadId).MustAsync(async (gameThreadId, cancellation) =>
            {
                if (gameThreadId == null)
                    return true;
                var gameThreadResult = await gameThreadRepository.FindByIdAsync(gameThreadId.Value);
                return gameThreadResult.IsSuccess;
            }).WithMessage(ValidationErrors.ThreadDoNotExist).WithName(ValidationKeys.GameThread);
        }
    }
}
