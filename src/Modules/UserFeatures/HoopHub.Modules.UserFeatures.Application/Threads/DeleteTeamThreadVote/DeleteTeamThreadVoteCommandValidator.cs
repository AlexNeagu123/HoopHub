using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.DeleteTeamThreadVote
{
    public class DeleteTeamThreadVoteCommandValidator : AbstractValidator<DeleteTeamThreadVoteCommand>
    {
        public DeleteTeamThreadVoteCommandValidator(ITeamThreadVoteRepository teamThreadVoteRepository, string fanId)
        {
            RuleFor(x => x.ThreadId).NotNull().NotEmpty().WithMessage(ValidationErrors.InvalidThreadId);
            RuleFor(x => x.ThreadId).MustAsync(async (commentId, cancellation) =>
            {
                var commentResult = await teamThreadVoteRepository.FindByIdAsyncIncludingAll(commentId, fanId);
                return commentResult.IsSuccess;
            }).WithMessage(ValidationErrors.CommentVoteDoNotExist).WithName(ValidationKeys.ThreadCommentVote);
        }
    }
}
