using Template.Infrastructure.Emails;
using Template.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMessaging(builder.Configuration, MessagingProvider.MassTransit)
    .AddEmails(builder.Configuration, EmailsProvider.SendGrid);

var app = builder.Build();

app.MapGet("test", () => "YES");

app.Run();
