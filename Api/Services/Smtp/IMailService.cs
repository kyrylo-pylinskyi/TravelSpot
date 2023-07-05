using Api.Services.Smtp.Request;

namespace Api.Services.Smtp
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendVerificationEmailAsync(VerificationRequest request);
    }
}
