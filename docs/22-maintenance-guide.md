# 22 — Application Maintenance & Content Update Guide

## Overview
This guide provides instructions for updating portfolio content (Profile, Work Experience, Skills, Projects) and maintaining dependencies over time.

---

## 1. Updating Seed Data & Database Facts

Since initial data is seeded automatically by `PortfolioDataSeeder.cs`, you can update or add facts directly in the seeder class.

### Adding a New Project
Location: `[PortfolioDataSeeder.cs](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/backend/MeghaPortfolio.API/Infrastructure/Persistence/Data/PortfolioDataSeeder.cs)`

To add a new project:
```csharp
context.Projects.Add(new ProjectEntity
{
    Title = "New Microservice Project Name",
    Tagline = "Short project tagline",
    Architecture = "Clean Architecture / Microservices",
    Description = "Detailed summary of the problem and solution...",
    Technologies = new List<string> { "C#", ".NET 9", "PostgreSQL", "Kafka" },
    KeyContribution = "Engineered core data pipeline...",
    MeasurableResult = "Achieved sub-50ms API response latency.",
    IsFlagship = false,
    GitHubUrl = "https://github.com/meghasyamreddy/new-project"
});
```

---

## 2. Updating React UI Components

To modify styling, colors, or section titles:
* **Design Tokens & Colors:** Edit `[index.css](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/frontend/megha-portfolio-ui/src/index.css)` variables (`--accent-blue`, `--bg-primary`).
* **Hero Banner Text:** Edit `[Hero.jsx](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/frontend/megha-portfolio-ui/src/components/Hero.jsx)`.
* **Projects Layout:** Edit `[Projects.jsx](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/frontend/megha-portfolio-ui/src/components/Projects.jsx)`.

---

## 3. Dependency Updates

### Backend (.NET NuGet Packages)
To check and update outdated backend packages:
```powershell
cd backend/MeghaPortfolio.API
dotnet list package --outdated
```

### Frontend (npm Packages)
To check and update outdated frontend packages:
```powershell
cd frontend/megha-portfolio-ui
cmd /c npm outdated
```
