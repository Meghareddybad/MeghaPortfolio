using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MeghaPortfolio.API.Core.Application.Interfaces;
using MeghaPortfolio.API.Core.Application.Services;
using MeghaPortfolio.API.Infrastructure.Middleware;
using MeghaPortfolio.API.Infrastructure.Persistence.Data;
using MeghaPortfolio.API.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers
builder.Services.AddControllers();

// 2. Configure Rate Limiting Policy (Security Against API Abuse / Spam)
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddFixedWindowLimiter("ContactFormLimiter", limiterOptions =>
    {
        limiterOptions.PermitLimit = 5; // Max 5 contact submissions
        limiterOptions.Window = TimeSpan.FromMinutes(1); // Per 1 minute window
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 0;
    });
});

// 3. Configure CORS Policy for React Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",  // Vite Dev Server
                "http://localhost:3000",  // React Standard Dev Server
                "https://meghaportfolio.pages.dev" // Production Cloudflare Pages URL
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// 4. Add Custom Swagger / OpenAPI Metadata
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Megha Syam Reddy Badhuri — Senior .NET Engineer Portfolio API",
        Version = "v1",
        Description = "Enterprise ASP.NET Core 9 Web API showcasing 5 YOE microservices, TPL concurrency, EF Core PostgreSQL persistence, and React integration.",
        Contact = new OpenApiContact
        {
            Name = "Megha Syam Reddy Badhuri",
            Email = "meghasyamreddy.dev@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/megha-syam-reddy-badhuri-79531914b")
        }
    });
});

// 5. Register Entity Framework Core DbContext (DbContext Lifetime: Scoped)
var postgresConnectionString = builder.Configuration.GetConnectionString("PostgreSQL");

if (!string.IsNullOrEmpty(postgresConnectionString))
{
    builder.Services.AddDbContext<PortfolioDbContext>(options =>
        options.UseNpgsql(postgresConnectionString));
}
else
{
    // Local Developer Fallback Database (In-Memory)
    builder.Services.AddDbContext<PortfolioDbContext>(options =>
        options.UseInMemoryDatabase("MeghaPortfolioDb"));
}

// 6. Register Application Layer Dependencies (SOLID - Dependency Inversion)
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();

var app = builder.Build();

// 7. Global Exception Handling Middleware (MUST BE FIRST!)
app.UseMiddleware<GlobalExceptionMiddleware>();

// 8. Enable CORS Middleware (MUST BE BEFORE Routing!)
app.UseCors("AllowReactFrontend");

// 9. Enable Rate Limiter Middleware
app.UseRateLimiter();

// 10. Seed Database on Application Startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    PortfolioDataSeeder.SeedData(dbContext);
}

// 11. Configure HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Megha Portfolio API v1");
        c.DocumentTitle = "Megha Syam Reddy Badhuri - API Documentation";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
