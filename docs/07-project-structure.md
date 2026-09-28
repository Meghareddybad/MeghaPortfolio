# 07 — Project Structure

## Overview
This document describes the actual directory tree of `MeghaPortfolio` and explains the responsibility of each folder and file.

---

## 1. Directory Tree

```text
MeghaPortfolio/                         <-- Monorepo Root
 ├── .gitignore                         <-- Combined .NET and Node.js ignore rules
 ├── .github/
 │    └── workflows/
 │         └── ci-cd.yml                <-- GitHub Actions automated pipeline
 ├── backend/
 │    ├── MeghaPortfolio.sln            <-- Visual Studio Solution File
 │    ├── MeghaPortfolio.API/           <-- ASP.NET Core 9 Web API Project
 │    │    ├── Core/
 │    │    │    ├── Domain/Entities/  <-- Domain Models (Profile, Experience, Skill, Project, ContactMessage)
 │    │    │    └── Application/
 │    │    │         ├── DTOs/          <-- Data Transfer Objects
 │    │    │         ├── Interfaces/    <-- IPortfolioRepository, IPortfolioService
 │    │    │         └── Services/      <-- PortfolioService mapping entities to DTOs
 │    │    ├── Infrastructure/
 │    │    │    ├── Middleware/        <-- GlobalExceptionMiddleware (RFC 7807)
 │    │    │    └── Persistence/
 │    │    │         ├── Data/          <-- PortfolioDbContext, PortfolioDataSeeder
 │    │    │         └── Repositories/  <-- PortfolioRepository (.AsNoTracking queries)
 │    │    ├── Controllers/             <-- Profile, Experience, Skills, Projects, Contact
 │    │    ├── Properties/launchSettings.json <-- Ports HTTP 5050 / HTTPS 7050
 │    │    ├── appsettings.json         <-- Application configuration
 │    │    ├── Dockerfile               <-- Multi-stage production container build
 │    │    ├── .dockerignore            <-- Build artifact exclusions
 │    │    ├── Program.cs               <-- Entry point, DI container, Middleware
 │    │    └── MeghaPortfolio.API.csproj
 │    └── MeghaPortfolio.Tests/         <-- NUnit 3 Test Project
 │         ├── PortfolioServiceTests.cs <-- Service unit tests (AAA pattern)
 │         ├── ProjectsControllerTests.cs<-- Controller unit tests
 │         └── MeghaPortfolio.Tests.csproj
 └── frontend/
      └── megha-portfolio-ui/           <-- React 18 + Vite JavaScript SPA
           ├── src/
           │    ├── components/         <-- Navbar, Hero, Metrics, Experience, Skills, Projects, ContactForm, Footer
           │    ├── services/api.js     <-- Axios API client abstraction
           │    ├── App.jsx             <-- Root component & Promise.all fetcher
           │    ├── main.jsx            <-- React DOM entry point
           │    └── index.css           <-- Glassmorphism CSS design system
           ├── index.html               <-- HTML document root
           ├── package.json             <-- npm dependencies
           └── vite.config.js           <-- Vite bundler configuration
```

---

## 2. Important File Responsibilities

### Backend Files
* `Program.cs`: Bootstraps Kestrel, registers DI dependencies (`AddScoped`), configures CORS policies, sets up Rate Limiter (`ContactFormLimiter`), registers Swagger OpenAPI specs, and defines middleware pipeline execution order.
* `PortfolioDbContext.cs`: EF Core database context configuring entity mapping and table definitions (`DbSet<T>`).
* `PortfolioDataSeeder.cs`: Seeds startup database with candidate profile details and **SmartStore** flagship project facts.
* `GlobalExceptionMiddleware.cs`: Centralized exception handler returning RFC 7807 `ProblemDetails` error objects.
* `PortfolioRepository.cs`: Data access repository utilizing EF Core `.AsNoTracking()` for optimal read query execution.

### Frontend Files
* `src/services/api.js`: Axios instance client communicating with backend REST endpoints (`http://localhost:5050/api`) with offline dev mode fallback.
* `src/App.jsx`: Root component orchestrating parallel `Promise.all()` initial data load.
* `src/index.css`: Defines glassmorphism styling, CSS custom design variables (`--bg-primary: #0B0F17`), typography, and responsive grid layouts.
