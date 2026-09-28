# 14 — Error Handling & Logging

## Overview
This document explains how exceptions, input validation failures, and operational logs are processed across the backend and frontend.

---

## 1. Error Flow Diagram

```text
Incoming Request
       │
       ▼
GlobalExceptionMiddleware (Try Block)
       │
       ├──────► Controller Validation ──(Invalid Input)──► Returns HTTP 400 Bad Request
       │
       ├──────► Rate Limiter Check ─────(Quota Exceeded)─► Returns HTTP 429 Too Many Requests
       │
       └──────► Database Query ─────────(Unhandled Error)► Catch Block Triggers:
                                                           1. Logs ex via ILogger
                                                           2. Returns HTTP 500 ProblemDetails JSON
```

---

## 2. Backend Exception Handling (`GlobalExceptionMiddleware.cs`)

Located in `Infrastructure/Middleware/GlobalExceptionMiddleware.cs`, this custom middleware wraps the HTTP request pipeline:

```csharp
public async Task InvokeAsync(HttpContext context)
{
    try
    {
        await _next(context);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);
        await HandleExceptionAsync(context, ex);
    }
}
```

* **Development Mode:** Returns exception message details for local debugging.
* **Production Mode:** Masked detail string (`"Please contact the system administrator."`) to protect server security.

---

## 3. Frontend Error Handling (`ContactForm.jsx` & `api.js`)

In the React SPA:
* **API Offline Resilience:** `api.js` catches network connection errors and serves fallback static objects so the UI remains presentable.
* **Form Feedback:** `ContactForm.jsx` catches API error responses (`HTTP 400` / `HTTP 429`) and renders an error alert banner explaining the validation issue.
