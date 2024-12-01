var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("UserService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:UserService"]);
});

builder.Services.AddHttpClient("NovelService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:NovelService"]);
});

builder.Services.AddHttpClient("CommentService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:CommentService"]);
});

builder.Services.AddHttpClient("NotificationService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:NotificationService"]);
});

builder.Services.AddHttpClient("HistoryService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:HistoryService"]);
});

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
