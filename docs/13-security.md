# 13 — Security Review & Controls

## Overview
This document evaluates the security implementation of `MeghaPortfolio` across authentication, authorization, secret isolation, input validation, CORS, rate limiting, and exception sanitization.

---

## 1. Security Audit Matrix

| Security Area | Implementation Status | Technical Control Used |
| :--- | :--- | :--- |
| **Authentication & Authorization** | **Implemented** | JWT Bearer tokens with HMAC SHA-256 signatures & `[Authorize(Roles = "Admin")]`. |
| **Credential Isolation** | **Remediated** | Server-side `ADMIN_PASSWORD` env var; 0 secret strings exposed in React bundle. |
| **SQL Injection Prevention** | **Implemented** | EF Core LINQ parameterized SQL query generation (`@p0`, `@p1`). |
| **Cross-Site Scripting (XSS)** | **Implemented** | React JSX automatic DOM string escaping. |
| **API Rate Limiting** | **Implemented** | ASP.NET Core `FixedWindowLimiter` (5 req/min quota on `POST /api/contact`). |
| **Cross-Origin Resource Sharing** | **Implemented** | Restricted to explicit trusted origins and `vercel.app` domain matching. |
| **Information Disclosure** | **Implemented** | `GlobalExceptionMiddleware` sanitizes 500 errors to RFC 7807 `ProblemDetails`. |
| **HTTPS Redirection** | **Implemented** | `app.UseHttpsRedirection()` enforced in pipeline. |

---

## 2. Admin Credential Exposure Remediation Report

### A. Previous Problem & Vulnerability
In a previous UI iteration, default helper text rendered static administrator credentials inside the React `AdminDashboard.jsx` component.

### B. Security Impact & Risk
Exposing credentials on a public frontend allows unauthorized third parties to authenticate as Administrator, access recruiter contact messages, and delete database records.

### C. Technical Root Cause
The React component tree rendered JSX helper text containing default login instructions, shipping secret strings directly to the client's web browser JavaScript bundle.

### D. Applied Fix & Remediation Steps
1. **Frontend Secret Purge**: Removed the rendered credential helper text from `AdminDashboard.jsx`.
2. **Server-Side Password Rotation**: Invalidated the compromised credential in `AuthController.cs` and changed fallback validation to read from server-side environment variables (`ADMIN_PASSWORD`).
3. **Frontend Bundle Audit**: Verified `npm run build` production output contains zero credential strings.
4. **RBAC Enforcement**: All administrative endpoints (`GET /api/contact`, `PATCH /api/contact/{id}/read`, `DELETE /api/contact/{id}`) require a valid signed JWT Bearer Token containing `ClaimTypes.Role = "Admin"`.

---

## 3. In-Depth Security Controls

### A. Authentication & JWT Tokens
Admin login requests (`POST /api/auth/login`) validate credentials server-side and return a signed 256-bit JWT Bearer Token valid for 8 hours.

### B. Rate Limiting (`POST /api/contact`)
`Program.cs` registers `ContactFormLimiter` allocating 5 requests per 1-minute window. Exceeding requests return `HTTP 429 Too Many Requests`.

### C. Information Leakage Prevention
`GlobalExceptionMiddleware` catches unhandled exceptions and outputs a clean JSON RFC 7807 `ProblemDetails` response, hiding stack traces in production.
