using HoopHub.Modules.UserAccess.Application.Services.Emails;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Emails
{
    public class EmailSender(IConfiguration configuration) : IEmailSender
    {
        private readonly IConfiguration _configuration = configuration;
        public async Task<bool> SendEmail(string subject, string destinationEmail, string username, string emailContent)
        {
            var apiKey = _configuration["SendGridApiKey"]!;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("nalex4080@gmail.com", "HoopHub");
            var to = new EmailAddress(destinationEmail, username);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", emailContent);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
    }
}
