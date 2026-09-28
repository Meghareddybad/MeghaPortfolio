# 12 — Authentication & Authorization

## Current Status
> **Authentication & Authorization are currently NOT implemented.**

---

## 1. Architectural Justification
The current iteration of `MeghaPortfolio` is a **public-facing developer portfolio**. Its core purpose is to allow recruiters, engineering managers, and visitors to freely access information about candidate experience, technical skills, and featured projects without requiring user login or account creation.

Exposing public `GET` endpoints ensures zero friction for recruiters reviewing Megha's resume.

---

## 2. Protected Endpoints Security Strategy
While user login authentication is omitted for read endpoints, the write endpoint `POST /api/contact` is protected by **ASP.NET Core Rate Limiting** (`FixedWindowLimiter`: Max 5 submissions/min per IP) to prevent spam abuse.

---

## 3. Recommended Future Implementation (Planned for Admin CMS Dashboard)
If an internal content management system (CMS) is added in the future to allow Megha to dynamically edit projects or read contact messages from an admin UI, the recommended authentication design will be:

```text
Admin User
   │
   ├── POST /api/auth/login (Submits Email & Password)
   │
   └── ASP.NET Core AuthService
          │
          └── Generates Signed JWT Bearer Token (containing Role = "Admin" claim)
                 │
                 └── Admin UI passes "Authorization: Bearer <Token>" in headers
```

### Planned Authentication Stack:
* **JWT (JSON Web Tokens):** Claims-based stateless authentication.
* **BCrypt.Net:** Salted password hashing.
* **Role-Based Access Control (RBAC):** Controller enforcement via `[Authorize(Roles = "Admin")]`.
