# 09 — Backend Documentation

## Overview
The backend is an **ASP.NET Core 9 Web API** built with C# following **Modular Clean Architecture**.

---

## 1. Backend Layer Responsibilities

```text
MeghaPortfolio.API/
 ├── Controllers/            <-- Handles HTTP Requests, Routing, & Response Status Codes
 ├── Core/
 │    ├── Domain/Entities/   <-- Pure C# Entity Classes (Profile, Experience, Skill, Project, ContactMessage)
 │    └── Application/
 │         ├── DTOs/         <-- Data Transfer Objects (Insulates Domain Models)
 │         ├── Interfaces/   <-- Service & Repository Abstractions (IPortfolioRepository, IPortfolioService)
 │         └── Services/     <-- PortfolioService (Entity-to-DTO Mapping & Rules)
 └── Infrastructure/
      ├── Middleware/        <-- GlobalExceptionMiddleware (RFC 7807 Error Sanitization)
      └── Persistence/
           ├── Data/         <-- PortfolioDbContext & Startup PortfolioDataSeeder
           └── Repositories/ <-- PortfolioRepository (EF Core .AsNoTracking() Queries)
```

---

## 2. Program.cs Dependency Injection & Pipeline Setup

```csharp
var builder = WebApplication.CreateBuilder(args);

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(...);

// Rate Limiting Policy
builder.Services.AddRateLimiter(options => {
    options.AddFixedWindowLimiter("ContactFormLimiter", l => {
        l.PermitLimit = 5;
        l.Window = TimeSpan.FromMinutes(1);
    });
});

// CORS Policy
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactFrontend", p => p.WithOrigins(...).AllowAnyHeader().AllowAnyMethod());
});

// EF Core DbContext Registration (Scoped Lifetime)
var postgresConnectionString = builder.Configuration.GetConnectionString("PostgreSQL");
if (!string.IsNullOrEmpty(postgresConnectionString))
    builder.Services.AddDbContext<PortfolioDbContext>(o => o.UseNpgsql(postgresConnectionString));
else
    builder.Services.AddDbContext<PortfolioDbContext>(o => o.UseInMemoryDatabase("MeghaPortfolioDb"));

// Dependency Injection (SOLID - Dependency Inversion)
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();

var app = builder.Build();

// Middleware Order (Critical!)
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseCors("AllowReactFrontend");
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

## 3. Key Backend Design Patterns Implemented

1. **Repository Pattern:** Encapsulates EF Core LINQ queries, abstracting data persistence from domain logic.
2. **Service Pattern:** Encapsulates entity-to-DTO transformation and business rules.
3. **Dependency Inversion Principle (SOLID "D"):** Higher-level controllers and services depend on abstractions (`IPortfolioRepository`), allowing unit test mocking via `Moq`.
4. **Middleware Pattern:** Intercepts HTTP requests for exception handling, CORS headers, and rate limiting.
