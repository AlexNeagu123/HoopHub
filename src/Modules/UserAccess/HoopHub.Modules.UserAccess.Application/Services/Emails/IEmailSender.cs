namespace HoopHub.Modules.UserAccess.Application.Services.Emails
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string subject, string destinationEmail, string username, string emailContent);
    }
}
