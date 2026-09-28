# 21 — Troubleshooting Guide

## Overview
This document lists common issues, root cause diagnoses, and exact resolution steps for `MeghaPortfolio`.

---

## 1. Issue Matrix

### Issue 1: PowerShell script execution blocked on `npm`
* **Symptom:** Terminal error `File C:\Program Files\nodejs\npm.ps1 cannot be loaded because running scripts is disabled on this system.`
* **Cause:** Windows PowerShell ExecutionPolicy restricts running `.ps1` scripts by default.
* **Diagnosis:** Run `Get-ExecutionPolicy` in PowerShell.
* **Solution:** Run commands using the `cmd /c` wrapper:
  ```powershell
  cmd /c npm run dev
  ```
  Or change the PowerShell execution policy for the current process:
  ```powershell
  Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
  ```

---

### Issue 2: Port 5050 or 7050 Already in Use
* **Symptom:** `System.IO.IOException: Failed to bind to address http://localhost:5050: address already in use.`
* **Cause:** Another background ASP.NET Core API or process is running on port 5050.
* **Diagnosis:** Run `netstat -ano | findstr :5050` to find the process ID (PID).
* **Solution:** Edit `backend/MeghaPortfolio.API/Properties/launchSettings.json` and change `applicationUrl` to another free port (e.g. `http://localhost:5060`).

---

### Issue 3: Browser CORS Error when React calls Web API
* **Symptom:** Browser console error `Access to XMLHttpRequest at 'http://localhost:5050/api/projects' from origin 'http://localhost:5173' has been blocked by CORS policy.`
* **Cause:** React dev server origin `http://localhost:5173` is missing from the API CORS whitelist policy.
* **Diagnosis:** Inspect response headers in Browser DevTools Network tab for `Access-Control-Allow-Origin`.
* **Solution:** Verify `Program.cs` includes `http://localhost:5173` in `builder.Services.AddCors(...)` and that `app.UseCors("AllowReactFrontend")` is called **before** `app.MapControllers()`.

---

### Issue 4: Contact Form returns HTTP 429 Too Many Requests
* **Symptom:** Contact Form displays red alert: `HTTP 429 Too Many Requests`.
* **Cause:** Client submitted more than 5 message requests within 1 minute.
* **Solution:** Wait 60 seconds for the fixed window rate limiter to reset before submitting again.

---

### Issue 5: Contact Form POST returns HTTP 500 (`No route matches the supplied values`)
* **Symptom:** Submitting details in the "Get in Touch" contact section failed with an internal server error on the live API.
* **Cause:** `ContactController.cs` returned `CreatedAtAction(nameof(SubmitMessage), new { id = result.Id }, result)`. ASP.NET Core attempted to generate a `Location` header by searching for a route matching `{id}`. Because `SubmitMessage` was an HTTP POST endpoint taking `[FromBody]` (without an `{id}` path parameter) and no `GetContactMessageById` endpoint existed, ASP.NET Core threw `InvalidOperationException: No route matches the supplied values`.
* **Diagnosis:** Inspected API controller implementation and ASP.NET Core routing behavior for `CreatedAtAction` without a matching GET route.
* **Solution:** Replaced `CreatedAtAction` with `StatusCode(StatusCodes.Status201Created, result)` in `ContactController.cs`:
  ```csharp
  var result = await _portfolioService.SubmitContactMessageAsync(request, cancellationToken);
  return StatusCode(StatusCodes.Status201Created, result);
  ```
* **Verification:** `dotnet test` passed 6/6 tests. Live POST test to `https://megha-portfolio-api-hbb8.onrender.com/api/contact` returned `201 Created` with the saved payload.

---

### Issue 6: Vercel Direct URL Refresh Returns 404 Not Found
* **Symptom:** Navigating directly to sub-routes or refreshing the page on Vercel returned standard 404 page.
* **Cause:** Vercel static hosting looks for physical static HTML files matching the request route unless single-page application (SPA) rewrite rules are configured.
* **Diagnosis:** Verified client-side React router behavior vs static file server fallback on Vercel edge.
* **Solution:** Created `vercel.json` in `frontend/megha-portfolio-ui/vercel.json` specifying rewrite rule to `index.html`:
  ```json
  {
    "rewrites": [
      { "source": "/(.*)", "destination": "/index.html" }
    ]
  }
  ```
---

### Issue 7: Mobile Contact Form Submission Issue
* **Problem:** Mobile users accessing the deployed website on phones (Mobile Safari iOS, Chrome Android over mobile carrier networks) were unable to submit the contact form, while local desktop testing succeeded.
* **Root Cause:**
  1. **CORS Origin Policy Mismatch**: `Program.cs` CORS policy hardcoded `http://localhost:5173`, `http://localhost:3000`, and `https://meghaportfolio.pages.dev`, but was missing the production Vercel frontend origin `https://megha-portfolio-1jfs.vercel.app`. Mobile browsers strictly enforce preflight `OPTIONS` checks for `application/json` `POST` requests and blocked the call.
  2. **Short Axios Timeout on Render Cold Starts**: `api.js` Axios client timeout was set to `10000` (10 seconds). Render.com free-tier containers spin down after 15 minutes of inactivity and take 20–45 seconds to cold-start. When mobile users tapped submit after idle, Axios aborted prematurely with `ECONNABORTED`.
* **Fix**:
  1. Added `https://megha-portfolio-1jfs.vercel.app` and wildcard `.SetIsOriginAllowed(origin => host.EndsWith("vercel.app") ...)` to `Program.cs` CORS builder configuration.
  2. Increased Axios timeout from `10000` to `30000` (30 seconds) in `frontend/megha-portfolio-ui/src/services/api.js`.
  3. Added mobile input attributes (`autoComplete="email"`, `inputMode="email"`) and user-friendly error messages in `ContactForm.jsx`.
* **Files Changed**:
  - `backend/MeghaPortfolio.API/Program.cs`
  - `frontend/megha-portfolio-ui/src/services/api.js`
  - `frontend/megha-portfolio-ui/src/components/ContactForm.jsx`
  - `docs/21-troubleshooting.md`
* **Why Desktop Worked**: Local desktop testing used `http://localhost:5173` (which matched the dev CORS origin list) or had active backend connections that prevented cold-start timeouts.
* **Testing**: Verified with `dotnet test` (6/6 pass), `npm run build` (0 errors), and live POST request to production endpoint `https://megha-portfolio-api-hbb8.onrender.com/api/contact`.
* **Prevention**: Always configure dynamic CORS origin matching (`SetIsOriginAllowed`) for production frontend hosts, and set adequate timeouts for serverless / auto-scaling backend cold starts.


