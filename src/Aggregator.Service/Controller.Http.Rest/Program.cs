using Controller.Http.Rest.Aggregators;
using Controller.Http.Rest.Middleware;
using Controller.Http.Rest.Models;
using Controller.Http.Rest.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions();

// add injection for sending httprequests and my aggregator
builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
builder.Services.AddSingleton<IUserAggregator, UserAggregator>();
builder.Services.Configure<ExternalResourcesOptions>(
    builder.Configuration.GetSection("ExternalResourcesOptions"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Add a custom middleware for converting exceptions to rest http response codes
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
