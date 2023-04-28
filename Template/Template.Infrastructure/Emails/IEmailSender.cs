namespace Template.Infrastructure.Emails;

public interface IEmailSender
{
    Task<bool> SendEmail(SendEmailPayload payload);
}
