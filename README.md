# Megha Syam Reddy Badhuri — Senior Developer Portfolio

[![Build, Test & CI Pipeline](https://github.com/meghasyamreddy/MeghaPortfolio/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/meghasyamreddy/MeghaPortfolio/actions/workflows/ci-cd.yml)
![.NET 9.0](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![React 18](https://img.shields.io/badge/React-18-61DAFB?logo=react)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-4169E1?logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?logo=docker)

A full-stack developer portfolio web application built in **C# ASP.NET Core 9 Web API** and **React 18 (Vite JS)**. Designed with **Modular Clean Architecture**, Entity Framework Core 9 (`.AsNoTracking()` query optimizations), NUnit 3 automated unit testing, rate limiting, and multi-stage Docker containerization.

---

## 🌟 Key Engineering Metrics & Achievements

* **95% Processing Latency Reduction:** Achieved via C# Task Parallel Library (TPL) multithreading and concurrency.
* **85%+ Automated Unit Test Coverage:** Built using NUnit 3 and Moq (**100% Pass Rate across 6 test cases**).
* **Flagship Microservices Project:** Features **SmartStore**, an enterprise Clean Architecture e-commerce platform with YARP API Gateway, polyglot persistence (SQL Server + MongoDB), Apache Kafka event streaming, and raw binary magic byte file security.
* **₹0/Month Infrastructure Cost:** Hosted permanently on free-tier serverless cloud infrastructure.

---

## 🏗️ System Architecture

```text
 ┌──────────────────────────────────────────────────────────────┐
 │    React 18 SPA (megha-portfolio-ui)                         │
 │    - Axios Service Layer (src/services/api.js)               │
 │    - Parallel Promise.all Initial Data Fetching               │
 └──────────────────────────────┬───────────────────────────────┘
                                │ HTTPS REST API (Port 5050)
                                ▼
 ┌──────────────────────────────────────────────────────────────┐
 │    ASP.NET Core 9 Web API (MeghaPortfolio.API)               │
 │    - Middleware: GlobalExceptionMiddleware (RFC 7807)        │
 │    - Middleware: RateLimiter (ContactFormLimiter 5 req/min)  │
 │    - Core Layer: Service Mapping & DTO Encapsulation         │
 │    - Infrastructure Layer: PortfolioRepository               │
 └──────────────────────────────┬───────────────────────────────┘
                                │ Npgsql / ConnectionStrings__PostgreSQL
                                ▼
 ┌──────────────────────────────────────────────────────────────┐
 │    PostgreSQL Database (Neon.tech Serverless / In-Memory Dev)│
 └──────────────────────────────────────────────────────────────┘
```

---

## 🛠️ Technology Stack

* **Backend:** C#, .NET 9.0, ASP.NET Core Web API, Entity Framework Core 9 (`9.0.2`), Npgsql PostgreSQL (`9.0.2`), Swashbuckle Swagger (`7.2.0`), System.Threading.RateLimiting.
* **Frontend:** React 18, Vite 8, JavaScript (ES6+), Axios (`1.7.9`), Glassmorphism CSS Design Tokens (`index.css`).
* **Testing:** NUnit 3 (`4.2.2`), Moq (`4.21.0`), Microsoft.NET.Test.Sdk (`17.12.0`).
* **DevOps & Containers:** Multi-Stage Dockerfile, GitHub Actions CI/CD Pipeline (`ci-cd.yml`).

---

## 🚀 Quick Start (Local Setup)

### 1. Run Backend API (.NET Web API)
```powershell
cd backend/MeghaPortfolio.API
dotnet run
```
* **Swagger Documentation UI:** Open `http://localhost:5050/swagger`

### 2. Run NUnit Automated Unit Tests
```powershell
cd backend
dotnet test
```

### 3. Run Frontend SPA (React + Vite)
```powershell
cd frontend/megha-portfolio-ui
cmd /c npm install
cmd /c npm run dev
```
* **React Application UI:** Open `http://localhost:5173`

---

## 📚 Complete Project Documentation (`/docs`)

Comprehensive documentation is available in the `[docs/](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs)` directory:

1. `[01-project-overview.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/01-project-overview.md)` — High-level system overview
2. `[02-business-requirements.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/02-business-requirements.md)` — Business problem, target users, & success criteria
3. `[03-functional-requirements.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/03-functional-requirements.md)` — Feature breakdown
4. `[04-non-functional-requirements.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/04-non-functional-requirements.md)` — Performance, security, & bundle metrics
5. `[05-system-architecture.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/05-system-architecture.md)` — Architecture diagrams & component details
6. `[06-technology-stack.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/06-technology-stack.md)` — Tech stack selection justifications
7. `[07-project-structure.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/07-project-structure.md)` — Complete project directory tree
8. `[08-frontend-documentation.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/08-frontend-documentation.md)` — React components & Axios service layer
9. `[09-backend-documentation.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/09-backend-documentation.md)` — ASP.NET Core API architecture & DI
10. `[10-database-documentation.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/10-database-documentation.md)` — EF Core PostgreSQL schema & ER diagram
11. `[11-api-documentation.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/11-api-documentation.md)` — REST endpoint specifications & JSON samples
12. `[12-authentication-authorization.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/12-authentication-authorization.md)` — Current state & future JWT CMS roadmap
13. `[13-security.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/13-security.md)` — Security audit (SQLi, XSS, CORS, Rate Limiting)
14. `[14-error-handling-logging.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/14-error-handling-logging.md)` — Global Exception Middleware & RFC 7807 specs
15. `[15-testing.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/15-testing.md)` — NUnit 3 & Moq test suite details
16. `[16-git-github-workflow.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/16-git-github-workflow.md)` — Monorepo structure & GitHub Actions workflow
17. `[17-configuration-environment-variables.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/17-configuration-environment-variables.md)` — Environment variables & secret isolation
18. `[18-local-development-setup.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/18-local-development-setup.md)` — Beginner step-by-step setup guide
19. `[19-deployment.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/19-deployment.md)` — ₹0/month free cloud deployment guide
20. `[20-production-checklist.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/20-production-checklist.md)` — Pre-launch production verification checklist
21. `[21-troubleshooting.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/21-troubleshooting.md)` — Known issues & exact resolutions
22. `[22-maintenance-guide.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/22-maintenance-guide.md)` — Content updates & dependency maintenance
23. `[23-future-enhancements.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/23-future-enhancements.md)` — Admin CMS & Redis caching roadmap
24. `[24-interview-guide.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/24-interview-guide.md)` — Senior .NET technical interview Q&A guide
25. `[25-complete-system-flow.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/25-complete-system-flow.md)` — Plain language 10-step request flow
26. `[documentation-audit.md](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/docs/documentation-audit.md)` — Documentation audit report

---

## 👤 Author & Contact

* **Name:** Megha Syam Reddy Badhuri
* **Role:** Senior .NET & Backend Engineer
* **Location:** Hyderabad, India
* **Email:** `meghasyamreddy7@gmail.com`
* **LinkedIn:** [Megha Syam Reddy Badhuri](https://www.linkedin.com/in/megha-syam-reddy-badhuri-79531914b)
* **GitHub:** [meghasyamreddy](https://github.com/meghasyamreddy)
