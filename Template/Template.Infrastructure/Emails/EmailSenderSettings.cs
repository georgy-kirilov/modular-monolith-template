namespace Template.Infrastructure.Emails;

public class EmailSenderSettings
{
    public const string Section = "EmailSender";

    public required string ApiKey { get; init; }

    public required string FromEmail { get; init; }

    public required string FromName { get; init; }
}
