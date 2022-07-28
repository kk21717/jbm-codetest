// See https://aka.ms/new-console-template for more information
using Application.Command;
using Controller.Worker;
using Controller.Worker.EventListeners.AuthService;
using Domain.Services;
using Infrastructure.Repository.SQLServer;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        

        //add config read capability using options
        //services.AddOptions();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
            .Build();


        //get worker options from appsettings
        services.Configure<WorkerOptions>(configuration.GetSection("WorkerOprions"));


        //MassTransit-RabbitMQ Configuration for adding consumer
        //to listen events from other microservices
        services.AddMassTransit(config =>
        {
            config.AddConsumers(typeof(Program).Assembly);
            config.UsingRabbitMq((context, cfg) => {
                cfg.Host(configuration["RabbitEventBusOptions:HostAddress"]);
                cfg.ConfigureEndpoints(context);
            });
        });

        //Add MediatR for implementing mediator pattern along CQRS
        services.AddMediatR(typeof(Program));
        services.AddMediatR(Assembly.GetAssembly(typeof(BaseCommandHandler<,>))!);


        //Add SQLServerRepository implementation for IRepository
        services.AddScoped<IRepository, SQLServerRepository>();
        services.AddDbContext<SQLServerDBContext>(o => {
            o.UseSqlServer(configuration["SQLServerRepositoryOptions:ConnectionString"]);
        });

        //Add AutoMapper library to avoid manually setting
        //fields with the same names in different models
        services.AddAutoMapper(typeof(Program));

    })
 .Build()
 .Run();
