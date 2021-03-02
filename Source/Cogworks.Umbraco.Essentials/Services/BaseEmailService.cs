using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;

namespace Cogworks.Umbraco.Essentials.Services
{
    public class BaseEmailService
    {
        private readonly string _smtpFrom;

        public BaseEmailService()
        {
            var smtpSection = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;

            _smtpFrom = smtpSection?.From
                        ?? throw new SmtpException("Empty 'From' configuration");
        }

        public BaseEmailService(string smtpFrom)
            => _smtpFrom = smtpFrom;

        protected virtual void SendEmail(string recipientEmail, string body, string subject, string friendlyFrom)
        {
            var mailMessage = new MailMessage
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                From = new MailAddress(_smtpFrom, friendlyFrom)
            };

            mailMessage.To.Add(recipientEmail);

            new SmtpClient().Send(mailMessage);
        }
    }
}