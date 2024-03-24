namespace HoopHub.Modules.NBAData.Application.Constants
{
    public class ErrorMessages
    {
        public const string PlayerBioNotFound = "Player bio not found";
        public const string PlayerIdEmpty = "Player id is required";
        public const string StartSeasonGreaterThanEndSeason = "startSeason query parameter must be less than endSeason query parameter";
        public const string TeamIdEmpty = "Team id is required";
        public const string DateEmpty = "Date is required";
        public const string DateInvalidFormat = "Date must be in valid format (YYYY-MM-DD)";
        public const string HomeTeamApiIdEmpty = "Home team api id is required";
        public const string VisitorTeamApiIdEmpty = "Visitor team api id is required";
        public const string BoxScoreNotFound = "Box score not found";
    }
}
