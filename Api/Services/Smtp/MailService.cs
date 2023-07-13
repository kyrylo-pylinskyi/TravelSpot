using Api.Services.Smtp.Request;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Api.Services.Smtp
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendVerificationEmailAsync(VerificationRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\RegistrationTemplate.html";
            StreamReader reader = new StreamReader(FilePath);
            string MailText = reader.ReadToEnd();
            reader.Close();
            MailText = MailText.Replace("[username]", request.UserName)
                               .Replace("[email]", request.ToEmail)
                               .Replace("[code]", request.Code);

            var mail = new MailRequest()
            {
                ToEmail = request.ToEmail,
                Subject = "Your Email Verification Code",
                Body = MailText
            };

            await SendEmailAsync(mail);
        }
    }
}
