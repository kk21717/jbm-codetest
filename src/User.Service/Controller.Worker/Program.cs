// See https://aka.ms/new-console-template for more information
using Application.Command;
using Controller.Worker.EventListeners.AuthService;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;


//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", false)
//    .Build();

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // services.AddHostedService<Worker>();
        services.AddOptions();
        services.AddMassTransit(config =>
        {
            config.AddConsumers(typeof(Program).Assembly);
            config.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
        });

        //services.AddMassTransitHostedService();

        //Add MediatR for implementing mediator pattern along CQRS
        services.AddMediatR(typeof(Program));
        services.AddMediatR(Assembly.GetAssembly(typeof(BaseCommandHandler<,>))!);
    })
 .Build()
 .Run();
