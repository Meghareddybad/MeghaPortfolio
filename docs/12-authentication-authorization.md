# 12 — Authentication & Authorization

## Current Status
> **JWT Bearer Authentication & Role-Based Access Control (RBAC) are FULLY IMPLEMENTED.**

---

## 1. Architectural Justification
`MeghaPortfolio` uses a hybrid access model:
- **Public Endpoints (`GET /api/profile`, `GET /api/experience`, `GET /api/skills`, `GET /api/projects`, `POST /api/contact`)**: Open to recruiters and visitors for zero friction browsing. `POST /api/contact` is protected by ASP.NET Core Rate Limiting (5 submissions/min per IP).
- **Admin Management Endpoints (`GET /api/contact`, `GET /api/contact/{id}`, `PATCH /api/contact/{id}/read`, `DELETE /api/contact/{id}`)**: Protected by JWT Bearer Authentication and `[Authorize(Roles = "Admin")]` attribute enforcement.

---

## 2. Authentication Flow

```text
Admin User
   │
   ├── POST /api/auth/login (Submits Username & Password)
   │
   └── ASP.NET Core AuthController
          │
          └── Generates Signed JWT Bearer Token (Claim: Role = "Admin", Expires in 8 Hours)
                 │
                 └── React Admin UI stores Token in localStorage & passes "Authorization: Bearer <Token>"
```

### Authentication Stack:
* **JWT (JSON Web Tokens):** Claims-based stateless authentication (`Microsoft.AspNetCore.Authentication.JwtBearer`).
* **Symmetric HMAC SHA-256 Signing:** Signed using 256-bit secret key (`JWT_SECRET` environment variable).
* **Role-Based Access Control (RBAC):** Controller endpoint enforcement via `[Authorize(Roles = "Admin")]`.
