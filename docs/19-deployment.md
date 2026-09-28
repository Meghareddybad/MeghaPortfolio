# 19 — Free Cloud Deployment Architecture (₹0/Month)

## Overview
This document details how `MeghaPortfolio` is deployed continuously for **₹0/month** using free-tier cloud platforms.

---

## 1. Free Deployment Topology

```text
                  ┌──────────────────────────────────────────────────┐
                  │    React Single-Page Application                 │
                  │    (Cloudflare Pages / Vercel Free Tier)        │
                  │    - Global CDN, Unlimited Bandwidth             │
                  │    - Automatic SSL & Free Custom Domain          │
                  └────────────────────────┬─────────────────────────┘
                                           │ HTTPS REST Requests
                                           ▼
                  ┌──────────────────────────────────────────────────┐
                  │    ASP.NET Core 9 Web API                        │
                  │    (Render.com / Koyeb Docker Web Service)       │
                  │    - Multi-stage Docker Container (Port 8080)    │
                  │    - Free Tier 512 MB RAM                        │
                  └────────────────────────┬─────────────────────────┘
                                           │ Npgsql / ConnectionStrings__PostgreSQL
                                           ▼
                  ┌──────────────────────────────────────────────────┐
                  │    Serverless PostgreSQL Database                │
                  │    (Neon.tech Free Tier)                         │
                  │    - 0.5 GB Free Database Storage                │
                  │    - 100% Free Forever, No Credit Card Required  │
                  └──────────────────────────────────────────────────┘
```

---

## 2. Platform Comparison & Cost Analysis

| Component | Free Platform Selected | Free Tier Quota | Cost |
| :--- | :--- | :--- | :--- |
| **Frontend SPA** | Cloudflare Pages / Vercel | Unlimited bandwidth, 100 custom domains | **₹0/month** |
| **Backend API** | Render.com / Koyeb | 512 MB RAM, 750 free instance hours/month | **₹0/month** |
| **Database** | Neon.tech Serverless | 0.5 GB PostgreSQL storage, instant branching | **₹0/month** |

---

## 3. Step-by-Step Production Deployment

### A. Deploy Database (Neon.tech)
1. Register free account at [Neon.tech](https://neon.tech).
2. Create PostgreSQL database named `meghaportfolio`.
3. Copy the Connection String:
   `Host=ep-xxx.neon.tech;Database=meghaportfolio;Username=megha;Password=secret`

---

### B. Deploy Backend Web API (Render.com)
1. Register free account at [Render.com](https://render.com).
2. Connect GitHub repository `MeghaPortfolio`.
3. Select **Web Service** → Environment: **Docker**.
4. Set Dockerfile Path: `backend/MeghaPortfolio.API/Dockerfile`.
5. Add Environment Variable:
   * Key: `ConnectionStrings__PostgreSQL`
   * Value: `<Your Neon PostgreSQL Connection String>`
6. Deploy Service. Copy output URL: `https://megha-portfolio-api.onrender.com`.

---

### C. Deploy Frontend SPA (Cloudflare Pages)
1. Register free account at [Cloudflare](https://pages.cloudflare.com).
2. Create Pages project connected to GitHub repository.
3. Root directory: `frontend/megha-portfolio-ui`.
4. Build command: `npm run build` | Build output directory: `dist`.
5. Add Environment Variable:
   * Key: `VITE_API_URL`
   * Value: `https://megha-portfolio-api.onrender.com/api`
6. Deploy Project.
