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
        public static string CommentAddedThreadNotificationContent(string userName)
        {
            return $"{userName} just commented on your thread..";
        }
    }
}
