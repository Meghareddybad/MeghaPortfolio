# 16 — Git & GitHub Workflow

## Overview
This document outlines version control practices, repository organization, `.gitignore` rules, and the CI/CD pipeline automation.

---

## 1. Monorepo Repository Structure
The project uses a single Git monorepo containing both the .NET API backend and React frontend:

```text
MeghaPortfolio/
 ├── .gitignore                 <-- Root Git ignore file
 ├── .github/
 │    └── workflows/
 │         └── ci-cd.yml       <-- GitHub Actions CI/CD pipeline
 ├── backend/                   <-- ASP.NET Core API & NUnit Tests
 └── frontend/                  <-- React Vite SPA
```

---

## 2. Root `.gitignore` Rules
Location: `[MeghaPortfolio/.gitignore](file:///c:/Users/megha/OneDrive/Desktop/Resumes/MeghaPortfolio/.gitignore)`

Strictly excludes build artifacts and local configuration files:
* **.NET Ignores:** `bin/`, `obj/`, `out/`, `.vs/`, `appsettings.Development.json`
* **Node Ignores:** `node_modules/`, `dist/`, `.env.local`, `*.log`

---

## 3. GitHub Actions CI/CD Pipeline (`ci-cd.yml`)
On every `git push` to `main`, GitHub Actions triggers `.github/workflows/ci-cd.yml`:

```text
git push origin main
        │
        ▼
GitHub Actions CI Runner (ubuntu-latest)
        ├──────► Job 1: backend-ci
        │         ├── Setup .NET 9 SDK
        │         ├── dotnet restore backend/MeghaPortfolio.sln
        │         ├── dotnet build backend/MeghaPortfolio.sln
        │         └── dotnet test backend/MeghaPortfolio.sln (Runs 6 NUnit Tests)
        │
        └──────► Job 2: frontend-ci
                  ├── Setup Node.js 22
                  ├── npm ci (frontend/megha-portfolio-ui)
                  └── npm run build (Validates Vite production bundle)
```

---

## 4. Standard Commit Conventions

| Commit Prefix | Usage Example |
| :--- | :--- |
| `feat:` | `feat: add ContactForm rate limiter middleware` |
| `fix:` | `fix: update launchSettings.json dev ports to 5050` |
| `test:` | `test: add NUnit unit tests for PortfolioService` |
| `docs:` | `docs: add comprehensive project documentation` |
