using Template.Infrastructure.MessageBroker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessageBroker(builder.Configuration, MessageProvider.MassTransit);

var app = builder.Build();

app.Run();
