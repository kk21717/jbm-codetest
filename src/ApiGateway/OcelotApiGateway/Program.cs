using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add ocelot service
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddControllers();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//Add swagger for ocelot using MMLib.SwaggerForOcelot library
builder.Services.AddSwaggerForOcelot(builder.Configuration);


var app = builder.Build();

//insert the SwaggerForOcelot middleware to expose interactive documentation
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

////In case I need to pass headers to microservices in future
//app.UseSwaggerForOcelotUI(opt =>
//{
//    opt.DownstreamSwaggerHeaders = new[]
//    {
//      new KeyValuePair<string, string>("Auth-Key", "AuthValue"),
//  };
//});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
