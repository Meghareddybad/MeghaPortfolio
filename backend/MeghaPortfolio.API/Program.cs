using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MeghaPortfolio.API.Core.Application.Interfaces;
using MeghaPortfolio.API.Core.Application.Services;
using MeghaPortfolio.API.Infrastructure.Middleware;
using MeghaPortfolio.API.Infrastructure.Persistence.Data;
using MeghaPortfolio.API.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers
builder.Services.AddControllers();

// 2. Configure JWT Authentication & Authorization
var secretKey = builder.Configuration["Jwt:SecretKey"] 
    ?? Environment.GetEnvironmentVariable("JWT_SECRET") 
    ?? "SuperSecretKey_MeghaPortfolio_SeniorDotNetDeveloper_2026_SecureKey!";

var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "MeghaPortfolioAPI",
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "MeghaPortfolioAdmin",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

// 3. Configure Rate Limiting Policy (Security Against API Abuse / Spam)
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

// 4. Configure CORS Policy for React Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",  // Vite Dev Server
                "http://localhost:3000",  // React Standard Dev Server
                "https://megha-portfolio-1jfs.vercel.app", // Production Vercel Frontend URL
                "https://meghaportfolio.pages.dev" // Production Cloudflare Pages URL
              )
              .SetIsOriginAllowed(origin =>
              {
                  if (string.IsNullOrEmpty(origin)) return false;
                  try
                  {
                      var host = new Uri(origin).Host;
                      return host == "localhost" || host.EndsWith("vercel.app") || host.EndsWith("pages.dev");
                  }
                  catch
                  {
                      return false;
                  }
              })
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// 5. Add Custom Swagger / OpenAPI Metadata
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

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your valid JWT token."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 6. Register Entity Framework Core DbContext (DbContext Lifetime: Scoped)
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

// 7. Register Application Layer Dependencies (SOLID - Dependency Inversion)
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();

var app = builder.Build();

// 8. Global Exception Handling Middleware (MUST BE FIRST!)
app.UseMiddleware<GlobalExceptionMiddleware>();

// 9. Enable CORS Middleware (MUST BE BEFORE Routing!)
app.UseCors("AllowReactFrontend");

// 10. Enable Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// 11. Enable Rate Limiter Middleware
app.UseRateLimiter();

// 12. Seed Database on Application Startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
    PortfolioDataSeeder.SeedData(dbContext);
}

// 13. Configure HTTP Request Pipeline
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
app.MapControllers();

app.Run();
