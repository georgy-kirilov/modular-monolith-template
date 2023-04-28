﻿namespace Template.Infrastructure.Messaging;

public interface IPublisher
{
    Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : class;
}
