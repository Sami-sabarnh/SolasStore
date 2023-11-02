using Solas.BL.IRepositories;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Solas.EF.Repositories
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public async Task SendEmailConfirmationAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer))
            {
                client.Port = _smtpPort;
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword); // Corrected here
                client.EnableSsl = true;

                var message = new MailMessage
                {
                    From = new MailAddress(_smtpUsername), // Corrected here
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(toEmail);

                await client.SendMailAsync(message);
            }
        }

    }
}
