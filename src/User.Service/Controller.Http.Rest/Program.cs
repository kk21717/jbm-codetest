using Application.Command;
using Controller.Http.Rest.Middleware;
using Domain.Services;
using Infrastructure.EventBus.RabbitMQ;
using Infrastructure.Repository.SQLServer;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Lib.EventBus;
using System.Reflection;
using Application.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuration
builder.Services.AddOptions();

//Add MediatR for implementing mediator pattern along CQRS
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(
    Assembly.GetAssembly(typeof(BaseCommandHandler<,>))!, 
    Assembly.GetAssembly(typeof(BaseQueryHandler<,>))!);


//Add SQLServerRepository implementation for IRepository
builder.Services.AddScoped<IRepository, SQLServerRepository>();
builder.Services.AddDbContext<SQLServerDBContext>(o =>{
    o.UseSqlServer(builder.Configuration["SQLServerRepositoryOptions:ConnectionString"]);
});


// we do not need to publish event from this microservice
// but keep library as well as infrastructure.eventbus.rabbitmq project
// for possible future need to raise any new event
// Add RabbitMq as the EventBusProvider 
builder.Services.AddScoped<IEventBus, RabbitEventBus>();
// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitEventBusOptions:HostAddress"]);
    });
});

// I have not used auto mapper library
// but prefer to keep in for any future need to auto mapping
// Add AutoMapper library to avoid manually setting
// fields with the same names in different models
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Add a custom middleware for converting domain- and
//application- exceptions to standard rest http response codes
app.UseMiddleware<ApiExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
