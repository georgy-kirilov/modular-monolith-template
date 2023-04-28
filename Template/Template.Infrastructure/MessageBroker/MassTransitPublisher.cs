using MassTransit;

namespace Template.Infrastructure.MessageBroker;

internal sealed class MassTransitPublisher : IPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint) => _publishEndpoint = publishEndpoint;

    public Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : class =>
        _publishEndpoint.Publish(message, cancellationToken);
}
