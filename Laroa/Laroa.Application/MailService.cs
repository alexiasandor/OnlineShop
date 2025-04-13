using System;
using System.IO;
using System.Threading.Tasks;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Laroa.Application
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings?.Value ?? throw new ArgumentNullException(nameof(mailSettings));
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            if (mailRequest == null)
            {
                throw new ArgumentNullException(nameof(mailRequest));
            }

            // Validate and parse sender email
            var senderEmail = !string.IsNullOrWhiteSpace(_mailSettings.Mail)
                ? MailboxAddress.Parse(_mailSettings.Mail)
                : throw new InvalidOperationException("Invalid sender email address.");

            // Validate and parse recipient email
            var toEmail = !string.IsNullOrWhiteSpace(mailRequest.ToEmail)
                ? MailboxAddress.Parse(mailRequest.ToEmail)
                : throw new InvalidOperationException("Invalid recipient email address.");

            var email = new MimeMessage
            {
                Sender = senderEmail,
            };

            email.To.Add(toEmail);
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body,
            };

            // Attachments logic (if needed)
            /*
            if (mailRequest.Attachments != null)
            {
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            builder.Attachments.Add(file.FileName, ms.ToArray(), ContentType.Parse(file.ContentType));
                        }
                    }
                }
            }
            */

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
