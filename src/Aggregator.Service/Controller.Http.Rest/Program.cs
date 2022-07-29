using Controller.Http.Rest.Aggregators;
using Controller.Http.Rest.Models;
using Controller.Http.Rest.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions();

builder.Services.Configure<ExternalResourcesOptions>(
    builder.Configuration.GetSection("ExternalResourcesOptions"));


builder.Services.AddHttpClient("UserAccountsClient", config =>
{
    config.BaseAddress = new Uri("http://127.0.0.1:8030/user-api/profiles/1");
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});

builder.Services.AddScoped<IHttpClientServiceImplementation, HttpClientFactoryService>();

//builder.Services.AddHttpClient<IUserAggregator, UserAggregator>();

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
