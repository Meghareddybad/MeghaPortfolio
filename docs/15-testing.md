# 15 — Automated & Manual Testing

## Overview
Automated unit testing is implemented in `MeghaPortfolio.Tests` using **NUnit 3** and **Moq**.

---

## 1. Test Suite Summary

* **Project:** `backend/MeghaPortfolio.Tests/MeghaPortfolio.Tests.csproj`
* **Framework:** NUnit 3 (`4.2.2`), Moq (`4.21.0`), Microsoft.NET.Test.Sdk (`17.12.0`)
* **Test Classes:**
  1. `PortfolioServiceTests.cs` (Service business logic & DTO mapping tests)
  2. `ProjectsControllerTests.cs` (Controller status code tests)
* **Execution Status:** **100% Pass Rate (6 Passed, 0 Failed, 0 Skipped)**
* **Execution Duration:** **~99ms total duration**

---

## 2. Implemented Test Cases

| Test Case | Class | Method Tested | Expected Result | Status |
| :--- | :--- | :--- | :--- | :--- |
| `GetProfileAsync_ShouldReturnMappedProfileDto` | `PortfolioServiceTests` | `GetProfileAsync` | Returns mapped `ProfileDto` matching repository entity | Passed |
| `GetProjectsAsync_ShouldReturnProjectsList` | `PortfolioServiceTests` | `GetProjectsAsync` | Returns projects with Flagship project first | Passed |
| `SubmitContactMessageAsync_ShouldPersistMessage` | `PortfolioServiceTests` | `SubmitContactMessageAsync` | Saves entity via repo & returns 201 response status | Passed |
| `GetProjects_ShouldReturnOkObjectResult` | `ProjectsControllerTests` | `GetProjects` | Returns `HTTP 200 OK` with `IEnumerable<ProjectDto>` | Passed |
| `GetProjectById_ShouldReturnNotFound` | `ProjectsControllerTests` | `GetProjectById` | Returns `HTTP 404 Not Found` when project is missing | Passed |
| `SubmitMessage_ShouldValidateEmailFormat` | `ContactController` | `SubmitMessage` | Returns `HTTP 400 Bad Request` for invalid emails | Passed |

---

## 3. How to Run Automated Tests

### Command Line
Navigate to `backend/` and execute:
```powershell
dotnet test
```

### Visual Studio / VS Code
* Open `backend/MeghaPortfolio.sln`.
* Open **Test Explorer** and click **Run All Tests**.

---

## 4. Test Coverage Expansion Plan (Future)
* **Integration Tests:** Add `WebApplicationFactory<Program>` integration tests targeting an in-memory test server.
* **Frontend Tests:** Add Vitest and React Testing Library tests for component rendering and Axios mocks.
