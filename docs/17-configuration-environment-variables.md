# 17 — Configuration & Environment Variables

## Overview
This document explains configuration management across development and production environments.

---

## 1. Backend Configuration Files

### `backend/MeghaPortfolio.API/appsettings.json`
Stores baseline application settings committed to Git:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

### Environment Overrides (Secrets & Production)

In production or local development, settings are injected via environment variables without hardcoding secrets in source files:

| Configuration Setting | Environment Variable Name | Placeholder / Sample Value |
| :--- | :--- | :--- |
| PostgreSQL Connection | `ConnectionStrings__PostgreSQL` | `Host=<server>;Database=portfolio;Username=<user>;Password=<secret>` |
| ASP.NET Core Environment| `ASPNETCORE_ENVIRONMENT` | `Development` or `Production` |
| Server Binding URL | `ASPNETCORE_URLS` | `http://+:8080` (Docker standard) |

---

## 2. Frontend Environment Variables (`.env`)

Vite exposes environment variables prefixed with `VITE_`:

| Environment Variable | Local Dev Value | Production Value | Purpose |
| :--- | :--- | :--- | :--- |
| `VITE_API_URL` | `http://localhost:5050/api` | `https://megha-portfolio-api.onrender.com/api` | Base URL for Axios REST calls |

---

## 3. Security Best Practice
> **CRITICAL RULE:** Actual database passwords or cloud API keys must **NEVER** be committed to GitHub repositories. Use cloud host environment dashboards (e.g. Render, Cloudflare Pages, Koyeb) to manage production secrets safely.
