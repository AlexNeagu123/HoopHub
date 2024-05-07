using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.UpdateTeamThreadVote
{
    public class UpdateTeamThreadVoteCommandValidator : AbstractValidator<UpdateTeamThreadVoteCommand>
    {
        public UpdateTeamThreadVoteCommandValidator(ITeamThreadVoteRepository teamThreadVoteRepository, string fanId)
        {
            RuleFor(x => x.ThreadId).NotNull().NotEmpty().WithMessage(ValidationErrors.InvalidThreadId);
            RuleFor(x => x.ThreadId).MustAsync(async (threadId, cancellation) =>
            {
                var commentVoteResult = await teamThreadVoteRepository.FindByIdAsyncIncludingAll(threadId, fanId);
                return commentVoteResult.IsSuccess;
            }).WithMessage(ValidationErrors.CommentVoteDoNotExist);
        }
    }
}
