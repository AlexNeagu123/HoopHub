using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateTeamThreadVote
{
    public class CreateTeamThreadVoteCommandValidator : AbstractValidator<CreateTeamThreadVoteCommand>
    {
        public CreateTeamThreadVoteCommandValidator(ITeamThreadRepository teamThreadRepository, ITeamThreadVoteRepository teamThreadVoteRepository, string fanId)
        {
            RuleFor(x => x.ThreadId).NotEmpty().WithMessage(ValidationErrors.InvalidCommentId);
            RuleFor(x => x.ThreadId).MustAsync(async (commentId, cancellation) =>
            {
                var threadResult = await teamThreadRepository.FindByIdAsync(commentId);
                return threadResult.IsSuccess;
            }).WithMessage(ValidationErrors.ThreadDoNotExist).WithName(ValidationKeys.ThreadComment);

            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var threadVoteResult = await teamThreadVoteRepository.FindByIdAsyncIncludingAll(command.ThreadId, fanId);
                return !threadVoteResult.IsSuccess;
            }).WithMessage(ValidationErrors.VoteAlreadyGiven).WithName(ValidationKeys.ThreadCommentVote);
        }
    }
}
