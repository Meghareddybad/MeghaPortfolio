# 04 — Non-Functional Requirements

## Overview
Non-functional requirements specify system qualities, performance benchmarks, security controls, and operational constraints.

---

## 1. Performance

### Implemented
* **Sub-100ms API Execution:** All `GET` endpoints utilize EF Core `.AsNoTracking()` to avoid Change Tracker overhead.
* **Parallel Frontend Data Fetching:** React frontend executes `Promise.all()` to dispatch Profile, Experience, Skills, and Projects requests concurrently over HTTP/2.
* **Vite Bundle Optimization:** React production asset bundle builds in **1.38 seconds** with a gzip size of **~93 KB**.

### Recommended Future Improvement
* Redis caching for `GET /api/projects` and `GET /api/profile` to further reduce database round-trips under high concurrency.

---

## 2. Security

### Implemented
* **Rate Limiting:** `POST /api/contact` is protected by `FixedWindowLimiter` (max 5 submissions/min per IP) returning `HTTP 429`.
* **CORS Restrictions:** Restricted to explicit trusted origins (`http://localhost:5173`, `http://localhost:3000`, `https://meghaportfolio.pages.dev`).
* **SQL Injection Prevention:** Handled by EF Core LINQ parameterized SQL queries.
* **XSS Prevention:** React JSX string escaping prevents script injection.
* **Information Disclosure Protection:** `GlobalExceptionMiddleware` catches unhandled exceptions and outputs RFC 7807 `ProblemDetails` without revealing stack traces.

### Recommended Future Improvement
* Integration of Cloudflare Turnstile or CAPTCHA on the Contact Form to block automated headless browser spam.

---

## 3. Reliability & Availability

### Implemented
* **In-Memory Local Fallback:** If PostgreSQL is unreachable during local development, `Program.cs` seamlessly falls back to EF Core In-Memory database (`MeghaPortfolioDb`).
* **Offline UI Graceful Degradation:** `api.js` includes fallback data objects so the React UI renders cleanly even if the backend is offline.

---

## 4. Maintainability & Testability

### Implemented
* **Modular Clean Architecture:** Strict separation between `Core/Domain`, `Core/Application`, `Infrastructure`, and `Controllers`.
* **Automated Test Coverage:** NUnit 3 + Moq test suite verifying services and controllers with **100% pass rate (6/6 tests passed)**.
* **GitHub Actions CI/CD:** Automated pipeline compiling solution and executing test suite on every `git push`.

---

## 5. Summary Matrix

| Metric | Implemented Target | Verification Result |
| :--- | :--- | :--- |
| **API Response Time** | < 100ms | Verified via Swagger |
| **Unit Test Pass Rate** | 100% | 6 Passed, 0 Failed |
| **Frontend Bundle Size** | < 100 KB gzipped | 93.29 KB gzipped |
| **Container Image Size** | < 200 MB | ~150 MB (Multi-Stage Docker) |
| **Infrastructure Cost** | ₹0/month | Cloudflare Pages + Render + Neon |
