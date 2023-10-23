using MediaAPI.Configurations;
using MediaAPI_Service.Implementations;
using MediaAPI_Service.Interfaces;
using MediaAPI_Utilities.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configurationSection = builder.Configuration.GetSection(nameof(AppSettings));
builder.Services.Configure<AppSettings>(configurationSection);

var appSettings = new AppSettings();
builder.Configuration.GetSection(nameof(AppSettings)).Bind(appSettings);

//Registering IFileStorage dependency
builder.Services.AddScoped<IFileStorage, FileStorage>();
builder.Services.AddSingleton(new FileSystemWatcherService(appSettings.Path));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
