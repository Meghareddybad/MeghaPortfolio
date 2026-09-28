# 01 — Project Overview

## 1. What is this project?
This project is a personal professional developer portfolio website created for **Megha Syam Reddy Badhuri**, a Senior .NET & Backend Engineer with nearly 5 years of experience. 

It is a full-stack web application composed of a **React JavaScript frontend** single-page application and a **C# ASP.NET Core 9 Web API backend** backed by **Entity Framework Core 9 (PostgreSQL / In-Memory)**. The system showcases professional experience, engineering achievements (such as a 95% latency reduction via TPL multithreading and 85%+ NUnit test coverage), technical skills, and flagship projects like **SmartStore Enterprise Microservices Platform**.

## 2. Why was it created?
The project was created to fulfill two major goals:
1. **Professional Representation:** Present Megha's actual professional background, microservices accomplishments, and GenAI hackathon achievements in a sleek, recruiter-friendly manner.
2. **Technical Mastery Showcase:** Function as a live, production-grade full-stack project demonstrating Clean Architecture, REST API design, EF Core query optimization (`.AsNoTracking()`), rate limiting, automated testing, containerization, and ₹0/month hosting deployment.

## 3. Who uses it?
* **Recruiters & Talent Acquisition:** Quickly view candidate qualifications, downloadable resume links, and contact options.
* **Hiring Managers & Engineering Leaders:** Evaluate senior-level architectural capabilities, microservices experience, and code quality.
* **Technical Interviewers:** Inspect repository structure, NUnit unit testing practices, and API design.
* **Portfolio Owner (Megha Syam Reddy Badhuri):** Receive direct contact inquiries from prospective employers.

## 4. What can users do?
* View executive summary, title, and key performance metrics.
* Browse a detailed timeline of professional experience at Aspire Systems and Virtusa.
* Inspect a technical competency matrix categorized by domain.
* Explore interactive cards for featured projects, including the **SmartStore** microservices platform and GenAI Chatbot.
* Submit direct message inquiries through a rate-limited contact form.
* Access live OpenAPI/Swagger documentation to test REST endpoints directly.

## 5. Main Features

| Feature | Description | Status |
| :--- | :--- | :--- |
| **Hero & Executive Summary** | Headline title, short bio, social CTAs (LinkedIn, GitHub) | Implemented |
| **Key Performance Metrics** | Banner highlighting 95% latency reduction, 85%+ test coverage, 5 YOE | Implemented |
| **Experience Timeline** | Interactive history of positions at Aspire Systems and Virtusa | Implemented |
| **Technical Skills Matrix** | Grouped technical competencies from C# .NET to React and Kafka | Implemented |
| **Featured Projects Showcase** | Showcase cards highlighting flagship microservices & AI projects | Implemented |
| **Validated Contact Form** | Contact form with input validation and rate limiting (5 req/min) | Implemented |
| **OpenAPI / Swagger UI** | Interactive API documentation endpoint reader | Implemented |
| **Automated NUnit Suite** | Automated unit tests verifying services and controllers | Implemented |
| **Multi-Stage Dockerfile** | Production container packaging separating build SDK from runtime | Implemented |
| **GitHub Actions CI/CD** | Automated workflow compiling code and running tests on `git push` | Implemented |

## 6. High-Level System Explanation
The application operates on a decoupled architecture. The frontend React single-page app runs in the user's web browser. When the page loads, React dispatches asynchronous HTTP GET requests to the ASP.NET Core Web API. The API validates the requests, queries PostgreSQL (or local In-Memory DB) using Entity Framework Core with read-only `.AsNoTracking()` optimizations, and returns formatted JSON data. If a user submits a message, the contact controller validates the input, enforces rate limits, persists the message, and returns an HTTP 201 Created status.

## 7. Project Goals
* Achieve sub-100ms API response times across all read endpoints.
* Maintain 100% pass rate across automated NUnit test cases.
* Deploy for **₹0/month** using free cloud tiers (Cloudflare Pages, Render, Neon PostgreSQL).
* Provide complete architectural transparency for technical interviews.

## 8. Project Limitations
* **Authentication/Admin Panel:** Currently, the portfolio API is read-focused and does not include JWT login or an internal CMS dashboard (Planned for Future Enhancement).
* **Cold Starts:** Free-tier hosting on Render.com may experience brief cold-start delays if inactive for 15+ minutes.
