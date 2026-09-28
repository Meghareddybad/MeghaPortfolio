# 13 — Security Review & Controls

## Overview
This document evaluates the security implementation of `MeghaPortfolio` across input validation, CORS, rate limiting, and exception sanitization.

---

## 1. Security Audit Matrix

| Security Area | Implementation Status | Technical Control Used |
| :--- | :--- | :--- |
| **SQL Injection Prevention** | **Implemented** | EF Core LINQ parameterized SQL query generation (`@p0`, `@p1`). |
| **Cross-Site Scripting (XSS)** | **Implemented** | React JSX automatic DOM string escaping. |
| **API Rate Limiting** | **Implemented** | ASP.NET Core `FixedWindowLimiter` (5 req/min quota on `POST /api/contact`). |
| **Cross-Origin Resource Sharing** | **Implemented** | Restricted to explicit trusted origins in `Program.cs`. |
| **Information Disclosure** | **Implemented** | `GlobalExceptionMiddleware` sanitizes 500 errors to RFC 7807 `ProblemDetails`. |
| **Secrets Isolation** | **Implemented** | `.gitignore` ignores `appsettings.Development.json` & `.env.local`. |
| **HTTPS Redirection** | **Implemented** | `app.UseHttpsRedirection()` enforced in pipeline. |
| **Authentication / JWT** | **Not Implemented** | Public portfolio API by design (Planned for future Admin CMS). |

---

## 2. In-Depth Security Analysis

### A. SQL Injection Prevention
* **Risk:** Attacker inputs malicious SQL statements (`' OR '1'='1`) to dump or delete database tables.
* **Control:** `PortfolioRepository` uses EF Core LINQ extension methods (`.FirstOrDefaultAsync()`, `.ToListAsync()`). EF Core automatically converts all query variables into parameterized placeholders, preventing SQL Injection vulnerabilities.

### B. Rate Limiting (`POST /api/contact`)
* **Risk:** Automated spambots submitting thousands of fake contact messages, causing database bloat.
* **Control:** `Program.cs` registers `ContactFormLimiter` allocating a quota of 5 requests per 1-minute window per IP. Exceeding requests are rejected immediately with `HTTP 429 Too Many Requests`.

### C. Information Leakage Prevention
* **Risk:** Unhandled exceptions outputting internal C# database connection strings or stack traces.
* **Control:** `GlobalExceptionMiddleware` catches all unhandled exceptions, logs the details silently via `ILogger`, and outputs a clean JSON response:
```json
{
  "status": 500,
  "title": "An unexpected error occurred while processing your request.",
  "detail": "Please contact the system administrator.",
  "instance": "/api/contact"
}
```
