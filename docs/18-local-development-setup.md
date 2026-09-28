# 18 — Local Development Setup Guide

## Overview
This step-by-step guide allows a developer to clone, build, run, and verify `MeghaPortfolio` on a local machine from scratch.

---

## 1. Prerequisites

Before running the application, install:
* **.NET 9.0 SDK:** [Download .NET 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
* **Node.js (v18+ or v22+):** [Download Node.js](https://nodejs.org/)
* **Git:** [Download Git](https://git-scm.com/)

---

## 2. Step-by-Step Setup

### Step 1: Clone Repository
```powershell
git clone https://github.com/meghasyamreddy/MeghaPortfolio.git
cd MeghaPortfolio
```

---

### Step 2: Build & Run Backend API (.NET Web API)
1. Navigate to the backend API directory:
   ```powershell
   cd backend/MeghaPortfolio.API
   ```
2. Build the solution:
   ```powershell
   dotnet build
   ```
3. Run the backend Web API server:
   ```powershell
   dotnet run
   ```
4. Verify Swagger UI in browser:
   * Open **`http://localhost:5050/swagger`**

---

### Step 3: Run Automated NUnit Unit Tests
Open a new terminal window:
```powershell
cd backend
dotnet test
```
Verify output shows: **`Passed! - Failed: 0, Passed: 6`**.

---

### Step 4: Run Frontend SPA (React + Vite)
1. Open a new terminal window and navigate to the React app directory:
   ```powershell
   cd frontend/megha-portfolio-ui
   ```
2. Install npm dependencies:
   ```powershell
   cmd /c npm install
   ```
3. Start the Vite development server:
   ```powershell
   cmd /c npm run dev
   ```
4. Verify React UI in browser:
   * Open **`http://localhost:5173`**

---

## 3. Common Startup Issues & Solutions

* **Issue 1: PowerShell script execution policy blocks `npm` (`npm.ps1 cannot be loaded`).**
  * *Solution:* Prefix npm commands with `cmd /c npm <command>` or run `Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass` in PowerShell.
* **Issue 2: Port 5050 already in use.**
  * *Solution:* Edit `backend/MeghaPortfolio.API/Properties/launchSettings.json` and change `applicationUrl` to an available port.
