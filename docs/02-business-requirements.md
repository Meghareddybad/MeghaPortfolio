# 02 — Business Requirements

## 1. Business Problem
Senior software engineers often struggle to differentiate their experience from generic resumes when applying for competitive enterprise or international roles. Standard resume documents cannot demonstrate real-time API execution, Clean Architecture design, code quality, or automated testing discipline.

## 2. Business Objective
The primary business objective of this application is to serve as an active proof-of-work platform that:
* Increases recruiter engagement by presenting verified metrics (95% latency reduction, 85%+ test coverage).
* Reduces technical screening friction by allowing interviewers to inspect live OpenAPI endpoints and clean source code.
* Operates at **zero ongoing infrastructure cost (₹0/month)**.

## 3. Target Users

### A. Technical Recruiters
* **Goal:** Quickly confirm candidate qualifications, total years of experience (5 YOE), location (Hyderabad, India), and contact details.

### B. Engineering Managers & Technical Interviewers
* **Goal:** Verify software design capability, SOLID principle compliance, database optimization (`.AsNoTracking()`), and testing practices.

### C. Candidate (Megha Syam Reddy Badhuri)
* **Goal:** Receive legitimate job inquiries, showcase actual achievements, and defend system architecture during technical interviews.

## 4. User Needs

| User Type | Primary Need | Implemented Solution |
| :--- | :--- | :--- |
| **Recruiter** | Immediate overview of candidate level & skills | Hero banner with executive metrics & quick contact button |
| **Interviewer** | Proof of code quality & architecture | Public GitHub monorepo with modular Clean Architecture & NUnit tests |
| **Visitor** | Fast loading responsive interface | React SPA with glassmorphism design tokens & < 1.5s build time |

## 5. Business Rules Implemented
1. **Fact Integrity Rule:** All professional data (companies, roles, dates, metrics) must strictly match verified resume sources without artificial inflation.
2. **Contact Protection Rule:** The contact form endpoint (`POST /api/contact`) must enforce rate limiting (max 5 requests/min per IP) to prevent spam bots from overloading the server.
3. **Graceful Fallback Rule:** If the backend database or server is unreachable, the React frontend must fall back gracefully to cached mock data rather than displaying broken white screens.

## 6. Success Criteria
* **Zero Host Cost:** Total hosting overhead must not exceed ₹0/month.
* **100% Test Quality Gate:** CI/CD pipeline must fail build if any unit test fails.
* **Sub-100ms API Speed:** REST GET endpoints must respond in under 100 milliseconds.
