# 06 — Technology Stack

## Overview
This document outlines every technology, framework, language, and tool used in `MeghaPortfolio`.

---

## 1. Technology Matrix

| Technology | Role | Version | Selection Justification |
| :--- | :--- | :--- | :--- |
| **C#** | Backend Programming Language | .NET 9.0 | Strongly typed enterprise language matching candidate's 5 YOE resume core. |
| **ASP.NET Core Web API** | Backend Web Framework | 9.0 | High-performance, cross-platform framework for building RESTful microservices. |
| **Entity Framework Core** | Object-Relational Mapper (ORM) | 9.0.2 | Provides LINQ queries, change tracking, and seamless PostgreSQL provider integration. |
| **PostgreSQL (`Npgsql`)** | Production Database | 9.0.2 | Industrial-grade SQL relational database with serverless zero-cost cloud hosting. |
| **EF Core In-Memory** | Dev Fallback Database | 9.0.2 | Enables zero-downtime local development without requiring local database setup. |
| **Swashbuckle Swagger** | API Documentation | 7.2.0 | Automatically generates interactive OpenAPI documentation UI. |
| **React.js** | Frontend UI Framework | 18.3.1 | Industry standard library for building reactive single-page applications. |
| **Vite** | Frontend Build Tool | 8.3.1 | Next-generation frontend bundler providing < 1.5s lightning-fast production builds. |
| **JavaScript (ES6+)** | Frontend Language | ES2022 | Matches candidate's resume (`JavaScript ES6+, React.js`), keeping UI code lean. |
| **Axios** | HTTP Client | 1.7.9 | Promise-based HTTP client providing clean request/response abstraction. |
| **NUnit 3** | Unit Testing Framework | 4.2.2 | Primary C# test framework matching candidate's resume test strategy. |
| **Moq** | Mocking Library | 4.21.0 | Enables mocking `IPortfolioRepository` and interfaces for isolated unit tests. |
| **Docker** | Containerization | 24.0+ | Guarantees identical execution environment in dev, staging, and production. |
| **GitHub Actions** | CI/CD Automation | v4 | Automated workflow running tests and builds on every `git push`. |

---

## 2. Technology Deep-Dives

### A. ASP.NET Core 9 Web API
* **What is it?** Microsoft's open-source, high-performance web framework.
* **Why used?** It forms the foundation of Megha's backend skills. Offers native Dependency Injection, middleware pipelines, and rate limiting.
* **Alternatives:** Node.js Express, Java Spring Boot, Python FastAPI.
* **Why selected?** Demonstrates candidate's core .NET C# engineering background.

### B. PostgreSQL & EF Core 9
* **What is it?** Advanced open-source relational database paired with Microsoft's ORM.
* **Why used?** Provides ACID-compliant schema persistence for Profile, Experience, Skills, Projects, and Contact Messages.
* **Alternatives:** SQL Server, MongoDB, SQLite.
* **Why selected?** Open-source industry leader with 100% free serverless hosting (Neon.tech).

### C. React 18 & Vite
* **What is it?** Component-driven UI library bundled with Vite.
* **Why used?** Renders a responsive glassmorphism portfolio UI with parallel API fetching.
* **Alternatives:** Angular, Vue.js, Next.js.
* **Why selected?** Matches candidate's resume skills (`React.js`, `JavaScript ES6+`).
