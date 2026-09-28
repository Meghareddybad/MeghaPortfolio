# 03 — Functional Requirements

## Overview
This document details every user-facing and backend functional requirement implemented in `MeghaPortfolio`.

---

## 1. Feature: Profile & Executive Summary (`GET /api/profile`)

* **Purpose:** Returns the candidate's executive title, bio summary, location, email, phone, LinkedIn, GitHub URLs, and primary metrics.
* **Who can use it:** Public (Recruiters, Visitors, Interviewers).
* **Input:** None.
* **Processing:** Queries `Profiles` DbSet using EF Core `.AsNoTracking()`, maps entity to `ProfileDto`.
* **Output:** JSON object containing `ProfileDto` (`HTTP 200 OK`).
* **Error Scenarios:** `HTTP 404 Not Found` if profile table is unseeded.
* **Status:** Implemented.

---

## 2. Feature: Work Experience Timeline (`GET /api/experience`)

* **Purpose:** Returns chronologically ordered work experience entries at Aspire Systems and Virtusa.
* **Who can use it:** Public.
* **Input:** None.
* **Processing:** Queries `Experiences` DbSet using `.AsNoTracking()`, projects list of `ExperienceDto`.
* **Output:** JSON array of `ExperienceDto` (`HTTP 200 OK`).
* **Error Scenarios:** Returns empty JSON array `[]` if no experience records exist.
* **Status:** Implemented.

---

## 3. Feature: Technical Competency Matrix (`GET /api/skills`)

* **Purpose:** Returns technical skills categorized by Backend, Databases, Architecture, Testing, Cloud, DevOps, and Frontend.
* **Who can use it:** Public.
* **Input:** None.
* **Processing:** Queries `Skills` DbSet ordered by `DisplayOrder`.
* **Output:** JSON array of `SkillDto` (`HTTP 200 OK`).
* **Error Scenarios:** Returns empty JSON array `[]` if no skills exist.
* **Status:** Implemented.

---

## 4. Feature: Featured Projects Showcase (`GET /api/projects` & `GET /api/projects/{id}`)

* **Purpose:** Displays project cards including **SmartStore Enterprise Microservices Platform** (Flagship), Enterprise Admin Platform, and GenAI Chatbot.
* **Who can use it:** Public.
* **Input:** Optional `id` path parameter for single project lookup.
* **Processing:** Queries `Projects` DbSet ordered with flagship projects first (`IsFlagship = true`).
* **Output:** JSON array or single `ProjectDto` (`HTTP 200 OK`).
* **Error Scenarios:** `HTTP 404 Not Found` for invalid project ID.
* **Status:** Implemented.

---

## 5. Feature: Rate-Limited Contact Form (`POST /api/contact`)

* **Purpose:** Allows recruiters and visitors to send direct messages to Megha.
* **Who can use it:** Public (Rate Limited: Max 5 submissions/min per IP).
* **Input:** JSON payload (`senderName`, `senderEmail`, `subject`, `message`).
* **Processing:**
  1. RateLimiter checks client IP quota.
  2. Controller validates email format and non-empty string fields.
  3. Service maps request to `ContactMessageEntity` and sets `CreatedAt = DateTime.UtcNow`.
  4. Repository saves entity to database via `SaveChangesAsync()`.
* **Output:** `HTTP 201 Created` with `ContactMessageResponseDto` and `Location` header.
* **Error Scenarios:**
  * `HTTP 400 Bad Request` if email is missing or message length < 10 characters.
  * `HTTP 429 Too Many Requests` if client exceeds rate limit quota.
* **Status:** Implemented.

---

## 6. Functional Status Summary

| Functional Area | API Endpoint | Processing Layer | Status |
| :--- | :--- | :--- | :--- |
| **Profile** | `GET /api/profile` | Service -> Repo -> DbContext | Implemented |
| **Experience** | `GET /api/experience` | Service -> Repo -> DbContext | Implemented |
| **Skills** | `GET /api/skills` | Service -> Repo -> DbContext | Implemented |
| **Projects** | `GET /api/projects` | Service -> Repo -> DbContext | Implemented |
| **Project Details** | `GET /api/projects/{id}` | Service -> Repo -> DbContext | Implemented |
| **Contact Message** | `POST /api/contact` | Service -> Repo -> DbContext | Implemented |
