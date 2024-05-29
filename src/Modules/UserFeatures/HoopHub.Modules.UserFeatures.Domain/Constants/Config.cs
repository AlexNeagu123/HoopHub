namespace HoopHub.Modules.UserFeatures.Domain.Constants
{
    public class Config
    {
        public const string DateFormat = "yyyy-MM-dd";
        public const int ContentMinLength = 10;
        public const int ContentMaxLength = 500;
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 50;

        public const int RookieBadgeThreshold = 10;
        public const int RegularBadgeThreshold = 100;
        public const int ExpertBadgeThreshold = 1000;

        public const string CommentAddedThreadNotificationTitle = "New comment";
        public const string DefaultAvatarPhotoUrl = "https://hoophub.blob.core.windows.net/userphotos/default-profile.png";
        public const string ReplyAddedNotificationTitle = "New reply";
        public const string FollowedTeamGameEndsTitle = "A team you follow finished the game";
        public const string FollowedPlayerGoodGameTitle = "Impressive Performance";
        public const int CommentTruncateLength = 25;

        public static string FollowedTeamGameEndsContent(string teamName)
        {
            return $"{teamName} just finished the game, check the box scores";
        }

        public static string TruncateNotificationComment(string content)
        {
            var response = content[..Math.Min(CommentTruncateLength, content.Length)];
            if (CommentTruncateLength < content.Length)
                response += "...";
            return response;
        }
        public static string ReplyAddedNotificationTitleContent(string userName, string content)
        {
            return $"{userName} just replied to your comment \"{TruncateNotificationComment(content)}\"";
        }

        public static string CommentAddedThreadNotificationContent(string userName, string content)
        {
            return $"{userName} just commented on your thread \"{TruncateNotificationComment(content)}\"";
        }
    }
}
