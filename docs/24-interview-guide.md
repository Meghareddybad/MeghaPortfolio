# 24 — Senior .NET Technical Interview Guide

## Overview
This document contains 15 technical interview questions categorized by difficulty, with exact architectural answers based on `MeghaPortfolio`.

---

## 1. Beginner Level Questions

### Q1: "Explain what this project does and what technologies were used."
* **Short Answer:** *"It is a full-stack developer portfolio application built in C# ASP.NET Core 9 Web API and React 18 with Vite. It showcases my 5 YoE background, technical competencies, and flagship projects like SmartStore. It uses EF Core 9 targeting PostgreSQL with an In-Memory fallback, unit tested with NUnit and Moq, and packaged using multi-stage Docker containers."*

---

## 2. Intermediate Level Questions

### Q2: "Why did you use `AsNoTracking()` in your repository LINQ queries?"
* **Short Answer:** *"In EF Core, `AsNoTracking()` tells the DbContext not to track entity state changes in its internal Change Tracker. For read-only API requests, this reduces CPU allocation and speeds up query execution by 30-50%."*
* **Project Reference:** Implemented across all read methods in `[PortfolioRepository.cs](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/backend/MeghaPortfolio.API/Infrastructure/Persistence/Repositories/PortfolioRepository.cs)`.

---

### Q3: "Explain how Dependency Injection service lifetimes work in ASP.NET Core."
* **Short Answer:** *"ASP.NET Core supports Transient, Scoped, and Singleton lifetimes. `PortfolioDbContext`, `IPortfolioRepository`, and `IPortfolioService` are registered as **Scoped**, meaning one instance is created per HTTP request and disposed when the request completes. This guarantees thread-safety because `DbContext` is not thread-safe."*
* **Project Reference:** Configured in `[Program.cs](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/backend/MeghaPortfolio.API/Program.cs)`.

---

## 3. Advanced Level Questions

### Q4: "How does your Global Exception Middleware work?"
* **Short Answer:** *"Positioned at the very top of the HTTP pipeline in `Program.cs`, `GlobalExceptionMiddleware` wraps `await _next(context)` in a try/catch block. If an unhandled exception occurs, it logs details via `ILogger` and writes a standardized RFC 7807 `ProblemDetails` JSON response with an HTTP 500 status, masking internal C# stack traces from client browsers."*
* **Project Reference:** Implemented in `[GlobalExceptionMiddleware.cs](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/backend/MeghaPortfolio.API/Infrastructure/Middleware/GlobalExceptionMiddleware.cs)`.

---

### Q5: "How does `Promise.all()` improve performance in your React application?"
* **Short Answer:** *"When `App.jsx` mounts, `Promise.all()` dispatches initial REST requests (Profile, Experience, Skills, Projects) concurrently in parallel over HTTP/2. This eliminates sequential network waterfall delays, reducing total data load time to the duration of a single request (~100ms)."*
* **Project Reference:** Implemented in `[App.jsx](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/frontend/megha-portfolio-ui/src/App.jsx)`.
