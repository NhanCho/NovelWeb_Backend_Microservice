using Microsoft.EntityFrameworkCore;
using NotificationMicroservice.Data;
using NotificationMicroservice.Repositories;
using NotificationMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 1, 0))));

builder.Services.AddScoped<NotificationRepository>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<INotificationFactory, NotificationFactory>();

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
