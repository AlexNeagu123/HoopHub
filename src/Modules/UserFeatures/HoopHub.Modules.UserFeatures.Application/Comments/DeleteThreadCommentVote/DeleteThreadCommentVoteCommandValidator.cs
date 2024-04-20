using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.DeleteThreadCommentVote
{
    public class DeleteThreadCommentVoteCommandValidator : AbstractValidator<DeleteThreadCommentVoteCommand>
    {
        public DeleteThreadCommentVoteCommandValidator(IThreadCommentVoteRepository threadCommentVoteRepository, string fanId)
        {
            RuleFor(x => x.CommentId).NotNull().NotEmpty().WithMessage(ValidationErrors.InvalidCommentId);
            RuleFor(x => x.CommentId).MustAsync(async (commentId, cancellation) =>
            {
                var commentResult = await threadCommentVoteRepository.FindByIdAsyncIncludingAll(commentId, fanId);
                return commentResult.IsSuccess;
            }).WithMessage(ValidationErrors.CommentVoteDoNotExist);
        }
    }
}
