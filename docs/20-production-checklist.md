# 20 — Production Readiness Checklist

## Overview
This checklist MUST be verified prior to marking the portfolio ready for public recruiter display.

---

## 1. Frontend Checklist
* [x] Vite production bundle builds in under 2 seconds (`npm run build` -> 1.38s).
* [x] Responsive layout verified across Desktop, Tablet, and Mobile screens.
* [x] Glassmorphism design tokens (`index.css`) render consistently across Chrome, Edge, Safari, and Firefox.
* [x] Graceful fallback data handling in `api.js` verified if backend is offline.
* [x] Links to LinkedIn, GitHub, and projects open safely in new tabs (`target="_blank" rel="noreferrer"`).

## 2. Backend Checklist
* [x] ASP.NET Core solution builds with **0 Warnings, 0 Errors** on .NET 9 SDK.
* [x] `GlobalExceptionMiddleware` catches unhandled exceptions and outputs RFC 7807 `ProblemDetails`.
* [x] Rate Limiter middleware (`FixedWindowLimiter`) active on `POST /api/contact` (max 5 req/min).
* [x] CORS policy (`AllowReactFrontend`) configured for trusted domains.
* [x] OpenAPI / Swagger documentation active at `/swagger`.

## 3. Database & EF Core Checklist
* [x] Database context handles PostgreSQL connection string overrides seamlessly.
* [x] In-Memory fallback database enabled for local development.
* [x] Startup seed script (`PortfolioDataSeeder.cs`) populates resume facts and flagship project.
* [x] EF Core `.AsNoTracking()` applied to read queries.

## 4. Security & DevOps Checklist
* [x] Root `.gitignore` excludes `bin/`, `obj/`, `node_modules/`, and `appsettings.Development.json`.
* [x] Secrets and DB connection strings managed via environment variables.
* [x] Multi-stage Dockerfile verified.
* [x] GitHub Actions CI workflow passing automated test cases (**6 Passed, 0 Failed**).
