﻿using MassTransit;

namespace Template.Infrastructure.Messaging;

public abstract class Consumer<TMessage> : IConsumer<TMessage>
    where TMessage : class
{
    public abstract Task Consume(TMessage message);

    public Task Consume(ConsumeContext<TMessage> context) => Consume(context.Message);
}
