# 25 — Complete System Flow (Beginner's Guide)

## Overview
This document explains the entire journey of a user request from the web browser through the backend API down to the database in plain, beginner-friendly language.

---

## 1. The 10-Step Journey of a User Request

```text
Step 1: User types URL (http://localhost:5173) in browser.
        │
Step 2: Browser downloads index.html, index.css, and compiled React JS bundles.
        │
Step 3: React executes App.jsx and mounts the user interface in the DOM.
        │
Step 4: React dispatches parallel HTTP GET requests using Axios to http://localhost:5050/api.
        │
Step 5: ASP.NET Core receives the request on port 5050 and passes it to GlobalExceptionMiddleware.
        │
Step 6: CORS Policy verifies that http://localhost:5173 is allowed to communicate with the API.
        │
Step 7: Routing matches /api/projects to ProjectsController.GetProjects().
        │
Step 8: Controller calls PortfolioService, which queries PortfolioRepository using EF Core .AsNoTracking().
        │
Step 9: Database executes SQL query and returns entities. Service maps entities to DTOs.
        │
Step 10: Controller returns HTTP 200 OK + JSON payload. React updates state and renders cards!
```

---

## 2. Plain Language Explanation of Key Terms

* **Single-Page Application (SPA):** A web application that loads a single HTML page and dynamically updates content without reloading the page when users click links.
* **REST API:** A standardized way for web applications to exchange data using standard HTTP verbs (`GET` for reading, `POST` for submitting data).
* **DTO (Data Transfer Object):** A simple object used to pass data between software layers without exposing database tables directly.
* **ORM (Object-Relational Mapper):** A tool (like Entity Framework Core) that lets developers query database tables using C# code instead of writing raw SQL queries manually.
