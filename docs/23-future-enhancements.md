# 23 — Future Enhancements & Roadmap

## Overview
This document outlines realistic, high-value future enhancements planned for `MeghaPortfolio`.

---

## 1. Feature Roadmap Matrix

| Planned Feature | Description | Technical Complexity | Priority | Status |
| :--- | :--- | :--- | :--- | :--- |
| **Admin CMS Dashboard** | Secure React Admin Panel to manage projects, skills, and read messages | Medium | High | Planned |
| **JWT Authentication** | Admin login with JWT Bearer tokens and BCrypt password hashing | Medium | High | Planned |
| **Redis API Caching** | Cache `/api/projects` and `/api/profile` endpoints to eliminate DB lookups | Low | Medium | Planned |
| **Cloudflare Turnstile** | Smart bot protection on Contact Form replacing standard inputs | Low | Medium | Planned |
| **Email Notification Dispatch** | Automated background email dispatch via SendGrid/SMTP when contact form is submitted | Medium | Medium | Planned |
| **GenAI Assistant Widget** | Embedded RAG Chatbot querying candidate resume facts | High | Low | Planned |

---

## 2. Enhancement Details

### A. Admin CMS Dashboard & JWT Authentication
* **Why Useful:** Allows Megha to dynamically add projects or read contact messages from an admin UI without modifying seed code.
* **Architecture:**
  * Add `POST /api/auth/login` issuing signed JWT tokens (`sub`, `email`, `role=Admin`).
  * Enforce `[Authorize(Roles = "Admin")]` on write endpoints (`POST`, `PUT`, `DELETE`).
  * Build protected React admin route (`/admin`).

### B. Redis API Response Caching
* **Why Useful:** Stores serialized JSON API responses in Redis cache memory for 1 hour, reducing PostgreSQL database load to near zero under heavy recruiter traffic.
* **Architecture:** Add `IDistributedCache` or `AddOutputCache()` middleware in `Program.cs`.
