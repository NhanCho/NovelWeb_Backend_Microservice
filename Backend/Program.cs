using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Backend API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Cho phép frontend từ localhost:3000
              .AllowAnyMethod()                      // Cho phép bất kỳ phương thức HTTP
              .AllowAnyHeader();                     // Cho phép bất kỳ header
    });
});
builder.Services.AddHttpClient("UserService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:UserService"]);
});

builder.Services.AddHttpClient<INovelService, NovelService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:NovelService"]);
});

builder.Services.AddHttpClient<ICategoryService, CategoryService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:NovelService"]);
});

builder.Services.AddHttpClient<IChapterService, ChapterService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:NovelService"]);
});

builder.Services.AddHttpClient("CommentService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:CommentService"]);
});

builder.Services.AddHttpClient<INotificationService, NotificationService>("NotificationService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:NotificationService"]);
});

builder.Services.AddHttpClient<HistoryService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Microservices:HistoryService"]);
});

builder.Services.AddScoped<HistoryService>();

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowReactApp");
app.MapControllers();

app.Run();