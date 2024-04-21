using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadCommentVote
{
    public class CreateThreadCommentVoteCommandValidator : AbstractValidator<CreateThreadCommentVoteCommand>
    {
        public CreateThreadCommentVoteCommandValidator(IThreadCommentRepository threadCommentRepository, IThreadCommentVoteRepository threadCommentVoteRepository, string fanId)
        {
            RuleFor(x => x.CommentId).NotEmpty().WithMessage(ValidationErrors.InvalidCommentId);
            RuleFor(x => x.CommentId).MustAsync(async (commentId, cancellation) =>
            {
                var commentResult = await threadCommentRepository.FindByIdAsync(commentId);
                return commentResult.IsSuccess;
            }).WithMessage(ValidationErrors.CommentDoNotExist).WithName(ValidationKeys.ThreadComment);

            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var commentResult = await threadCommentVoteRepository.FindByIdAsyncIncludingAll(command.CommentId, fanId);
                return !commentResult.IsSuccess;
            }).WithMessage(ValidationErrors.VoteAlreadyGiven).WithName(ValidationKeys.ThreadCommentVote);
        }
    }
}
