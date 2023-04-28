namespace Template.Infrastructure.Messaging;

public sealed class MessageBrokerSettings
{
    public const string Section = "MessageBroker";

    public required string Host { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }
}
