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
        public const string InvalidDate = "Date must be in a valid format (yyyy-MM-dd)";
        public const string InvalidPlayerId = "Player Id cannot be empty";
        public const string InvalidPlayerRating = "Player rating should be a decimal between 0 and 10";
        public const string InvalidTitle = "Title should have at least 5 characters and at most 50";
        public const string InvalidThreadContent = "Thread content should have at least 10 characters and at most 500";
        public const string InvalidImageUrl = "Image URL cannot be empty";
        public const string InvalidTeamId = "Team Id cannot be empty";
        public const string InvalidCommentContent = "Comment content should have at least 10 characters and at most 500";
        public const string InvalidThreadId = "Thread Id cannot be empty";
        public const string InvalidParentCommentId = "Parent comment id cannot be empty";
        public const string InvalidCommentId = "Comment Id cannot be empty";
        public const string InvalidGameRating = "Game rating should be a decimal between 0 and 5";
        public const string InvalidPage = "Page shoukd be greater than 0";
        public const string InvalidPageSize = "Page size should be greater than 0";
    }
}
