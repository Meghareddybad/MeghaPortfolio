# 11 — API Documentation

## Overview
`MeghaPortfolio.API` provides RESTful JSON endpoints documented using OpenAPI/Swagger specs.

---

## 1. API Endpoints Table

| Method | Endpoint | Description | Auth Required | Rate Limited | Status |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `GET` | `/api/profile` | Fetches candidate bio, title, and key metrics | None | No | Implemented |
| `GET` | `/api/experience` | Fetches work experience timeline entries | None | No | Implemented |
| `GET` | `/api/skills` | Fetches technical skills matrix | None | No | Implemented |
| `GET` | `/api/projects` | Fetches featured projects list | None | No | Implemented |
| `GET` | `/api/projects/{id}` | Fetches single project by ID | None | No | Implemented |
| `POST` | `/api/contact` | Submits recruiter contact form message | None | Yes (5 req/min) | Implemented |

---

## 2. Endpoint Details & Payload Examples

### A. GET `/api/profile`
* **Response:** `HTTP 200 OK`
```json
{
  "id": 1,
  "fullName": "Megha Syam Reddy Badhuri",
  "title": "Senior .NET & Backend Engineer",
  "summary": "Results-driven Senior .NET & Backend Engineer with nearly 5 years of experience...",
  "location": "Hyderabad, India",
  "email": "meghasyamreddy7@gmail.com",
  "phone": "+91 9491364416",
  "linkedInUrl": "https://www.linkedin.com/in/megha-syam-reddy-badhuri-79531914b",
  "gitHubUrl": "https://github.com/meghasyamreddy",
  "yearsOfExperience": 5,
  "latencyReductionMetric": "95% Processing Latency Reduction via TPL Concurrency",
  "testCoverageMetric": "85%+ Automated Unit Test Coverage (NUnit/TDD)"
}
```

---

### B. GET `/api/projects`
* **Response:** `HTTP 200 OK`
```json
[
  {
    "id": 1,
    "title": "SmartStore — Enterprise Microservices Platform",
    "tagline": "Clean Architecture Fashion E-Commerce Platform in .NET 9 with Polyglot Persistence & Kafka",
    "architecture": "Clean Architecture / Decoupled Microservices with YARP API Gateway",
    "description": "Enterprise e-commerce platform exposing microservices behind YARP API Gateway...",
    "technologies": ["C#", ".NET 9", "YARP Gateway", "SQL Server", "MongoDB", "Apache Kafka", "React.js"],
    "keyContribution": "Architected multi-service solution with YARP gateway...",
    "measurableResult": "100% NUnit test pass rate, sub-100ms API response time.",
    "isFlagship": true,
    "gitHubUrl": "https://github.com/meghasyamreddy/SmartStore"
  }
]
```

---

### C. POST `/api/contact`
* **Request Body:**
```json
{
  "senderName": "Jane Doe",
  "senderEmail": "jane.doe@techcompany.com",
  "subject": "Senior .NET Opportunity",
  "message": "We reviewed your portfolio and microservices background."
}
```
* **Response:** `HTTP 201 Created`
```json
{
  "id": 1,
  "senderName": "Jane Doe",
  "senderEmail": "jane.doe@techcompany.com",
  "subject": "Senior .NET Opportunity",
  "message": "We reviewed your portfolio and microservices background.",
  "createdAt": "2026-09-28T14:00:00Z",
  "status": "Message Received Successfully"
}
```
* **Error Response (Rate Limited):** `HTTP 429 Too Many Requests`
