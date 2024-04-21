namespace HoopHub.Modules.UserFeatures.Domain.Constants
{
    public class ValidationErrors
    {
        public const string InvalidFanId = "Fan Id cannot be empty";
        public const string InvalidRecipientId = "Recipient Id cannot be empty";
        public const string InvalidSenderId = "Sender Id cannot be empty";
        public const string InvalidUsername = "Username cannot be empty";
        public const string InvalidEmail = "Email cannot be empty";
        public const string BothTeamIdsRequired = "Visitor or Home team ids cannot be empty";
        public const string InvalidDate = "Date must be in a valid format (yyyy-MM-dd) and should be earlier (or equal) than todays date";
        public const string InvalidPlayerId = "Player Id cannot be empty";
        public const string InvalidPlayerRating = "Player rating should be a decimal between 0 and 10";
        public const string InvalidTitle = "Title should have at least 5 characters and at most 50";
        public const string InvalidThreadContent = "Thread content should have at least 10 characters and at most 500";
        public const string InvalidImageUrl = "Image URL cannot be empty";
        public const string InvalidTeamId = "Team Id cannot be empty";
        public const string InvalidCommentContent = "Comment content should have at least 10 characters and at most 500";
        public const string InvalidThreadId = "Thread Id cannot be empty";
        public const string InvalidParentCommentId = "Parent comment id cannot be empty";
        public const string CommentDoNotExist = "Comment with this id does not exist";
        public const string InvalidCommentId = "Comment Id cannot be empty";
        public const string InvalidGameRating = "Game rating should be a decimal between 0 and 5";
        public const string InvalidPage = "Page shoukd be greater than 0";
        public const string InvalidPageSize = "Page size should be greater than 0";
        public const string ThreadUpdateNotAuthorized = "Only the creator of the thread can update it";
        public const string ThreadDeleteNotAuthorized = "Only the creator of the thread can delete it";
        public const string ShouldBeExactlyOneThreadNonNull = "Exactly one of TeamThreadId and GameThreadId should be non-null";
        public const string VoteAlreadyGiven = "Vote already given";
        public const string CommentVoteDoNotExist = "Comment vote does not exist";
        public const string InvalidFirstComment = "First comment does not exist";
        public const string ThreadDoNotExist = "Thread with this id does not exist";
        public const string GameReviewAlreadyExists = "Game review already exists";
        public const string GameReviewDoNotExist = "Game review does not exist";
        public const string PlayerPerformanceReviewExists = "Player performance review already exists";
        public const string PlayerPerformanceReviewDoNotExist = "Player performance review does not exist";
        public const string GameThreadExists = "Game thread already exists";
    }
}
