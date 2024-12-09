using Microsoft.EntityFrameworkCore;
using NovelMicroservice.Data;
using NovelMicroservice.Models;
using NovelMicroservice.Repositories;
using NovelMicroservice.Repositoriesl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 33))));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<INovelRepository, NovelRepository>();
builder.Services.AddScoped<IChapterRepository, ChapterRepository>(); // Đăng ký ChapterRepository


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
app.UseCors("AllowLocalhost");  // Áp dụng chính sách CORS đã cấu hình
