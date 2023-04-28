using Microsoft.Extensions.Logging;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace Template.Infrastructure.Emails;

internal sealed class SendGridEmailSender : IEmailSender
{
    private readonly EmailSenderSettings _settings;
    private readonly ILogger<SendGridEmailSender> _logger;

    public SendGridEmailSender(EmailSenderSettings settings, ILogger<SendGridEmailSender> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<bool> SendEmail(SendEmailPayload payload)
    {
        var client = new SendGridClient(_settings.ApiKey);

        var message = new SendGridMessage
        {
            From = new EmailAddress(_settings.FromEmail, _settings.FromName),
            Subject = payload.Subject,
        };

        message.AddTo(payload.ToEmail, payload.ToName);

        message.AddContent(payload.MimeType, payload.Body);

        var response = await client.SendEmailAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("An error has occurred while sending an email: {@payload}", payload);
        }

        return response.IsSuccessStatusCode;
    }
}
