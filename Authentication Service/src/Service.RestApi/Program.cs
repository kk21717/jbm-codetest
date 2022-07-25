using Domain.Core;
using Infrastructure.Repository.SQLServer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuration
builder.Services.AddOptions();

// Add SQLServerRepository implementation for IRepository
builder.Services.AddScoped<IRepository,SQLServerRepository>();
builder.Services.Configure<SQLServerRepositoryOptions>(
    builder.Configuration.GetSection("SQLServerRepositoryOptions"));

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration.GetValue<string>("RabbitEventBusOptions:HostAddress"));
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
