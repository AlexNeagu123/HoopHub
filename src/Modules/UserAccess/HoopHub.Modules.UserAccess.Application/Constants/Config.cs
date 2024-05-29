namespace HoopHub.Modules.UserAccess.Application.Constants
{
    public class Config
    {
        public const string JwtSecretKey = "JWT:Secret";
        public const string JwtIssuerKey = "JWT:ValidIssuer";
        public const string JwtAudienceKey = "JWT:ValidAudience";
        public const int UserNameMaxLength = 20;
    }
}
