using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Template.Infrastructure.Emails;

public static class EmailsRegistration
{
    public static IServiceCollection AddEmails(this IServiceCollection services,
        IConfiguration configuration,
        EmailsProvider emailsProvider)
    {
        services.Configure<EmailSenderSettings>(
            configuration.GetSection(EmailSenderSettings.Section));

        services.AddSingleton(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<EmailSenderSettings>>().Value);

        switch (emailsProvider)
        {
            case EmailsProvider.SendGrid:
                services.AddSingleton<IEmailSender, SendGridEmailSender>();
                break;

            default:
                throw new InvalidOperationException();
        }

        return services;
    }
}
