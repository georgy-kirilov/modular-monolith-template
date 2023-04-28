namespace Template.Infrastructure.Emails;

public sealed class SendEmailPayload
{
    public required string ToEmail { get; init; }

    public required string ToName { get; init; }

    public required string Subject { get; init; }

    public required string MimeType { get; init; }

    public required string Body { get; init; }
}
