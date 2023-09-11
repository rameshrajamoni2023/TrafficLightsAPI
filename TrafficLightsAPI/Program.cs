using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using TrafficLightsAPI.Data;
using TrafficLightsAPI.Model;
using TrafficLightsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure the Service for dependency injection
builder.Services.AddSingleton<TrafficLightService>();

builder.Services.AddDbContext<TrafficLightDbContext>(option =>
    option.UseSqlite("Filename=C:\\sqlite\\database\\myworks.db"), ServiceLifetime.Singleton); // builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<TrafficLightSettings>();


builder.Configuration.AddJsonFile("appsettings.json");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add the CORS policy in the ConfigureServices method
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



//builder.Services.Configure<TrafficLightSettings>(Configuration.GetSection("TrafficLightSettings"));

//logging
builder.Services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

var app = builder.Build();

// Add CORS middleware in the Configure method, before UseEndpoints
app.UseCors("AllowLocalhost");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();


}




app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
