using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetCommentsPagedByThread
{
    public class GetCommentsPagedByThreadQueryValidator : AbstractValidator<GetCommentsPagedByThreadQuery>
    {
        public GetCommentsPagedByThreadQueryValidator(IThreadCommentRepository threadCommentRepository, ITeamThreadRepository teamThreadRepository)
        {
            RuleFor(x => x.TeamThreadId).Must((command, teamThreadId) => (teamThreadId == null) ^ (command.GameThreadId == null)).WithMessage(ValidationErrors.ShouldBeExactlyOneThreadNonNull);
            RuleFor(x => x.Page).GreaterThan(0).WithMessage(ValidationErrors.InvalidPage);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(ValidationErrors.InvalidPageSize);
            RuleFor(x => x.FirstComment).MustAsync(async (firstComment, cancellation) =>
            {
                if (firstComment == null)
                    return true;

                var commentResult = await threadCommentRepository.FindByIdAsync(firstComment.Value);
                return commentResult.IsSuccess;
            }).WithMessage(ValidationErrors.InvalidFirstComment).WithName(ValidationKeys.ThreadComment);

            RuleFor(x => x.TeamThreadId).MustAsync(async (teamThreadId, cancellation) =>
            {
                if (teamThreadId == null)
                    return true;

                var teamThreadResult = await teamThreadRepository.FindByIdAsync(teamThreadId.Value);
                return teamThreadResult.IsSuccess;
            }).WithMessage(ValidationErrors.ThreadDoNotExist).WithName(ValidationKeys.TeamThread);
        }
    }
}
