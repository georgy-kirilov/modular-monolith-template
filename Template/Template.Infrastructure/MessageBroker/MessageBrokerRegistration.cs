using System.Reflection;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Template.Infrastructure.MessageBroker;

public static class MessageBrokerRegistration
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services,
        IConfiguration configuration,
        MessageProvider messageProvider)
    {
        services.ConfigureMessageBrokerSettings(configuration);

        switch (messageProvider)
        {
            case MessageProvider.MassTransit:
                services.AddMassTransitMessageProvider();
                break;

            default:
                throw new InvalidOperationException();
        }

        return services;
    }
    
    private static IServiceCollection ConfigureMessageBrokerSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(
            configuration.GetSection(MessageBrokerSettings.Section));

        services.AddSingleton(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        return services;
    }

    private static IServiceCollection AddMassTransitMessageProvider(this IServiceCollection services)
    {
        services.AddMassTransit(bus =>
        {
            bus.SetKebabCaseEndpointNameFormatter();

            bus.UsingRabbitMq((context, configurator) =>
            {
                var settings = context.GetRequiredService<MessageBrokerSettings>();

                configurator.Host(new Uri(settings.Host), host =>
                {
                    host.Username(settings.Username);
                    host.Password(settings.Password);
                });

                configurator.ConfigureEndpoints(context);
            });

            var consumerTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type =>
                    type.IsClass &&
                    !type.IsAbstract &&
                    type.BaseType?.IsGenericType == true &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(Consumer<>));

            foreach (var consumerType in consumerTypes)
            {
                bus.AddConsumer(consumerType);
            }
        });

        services.AddSingleton<IPublisher, MassTransitPublisher>();

        return services;
    }
}
