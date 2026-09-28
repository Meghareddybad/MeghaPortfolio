# 08 — Frontend Documentation

## Overview
The frontend application (`megha-portfolio-ui`) is built using **React 18** and **Vite** in modern **JavaScript (ES6+)**.

---

## 1. Frontend Component Hierarchy

```text
index.html
 └── main.jsx
      └── App.jsx (Parallel Promise.all Fetcher)
           ├── Navbar.jsx (Sticky Glass Navigation)
           ├── Hero.jsx (Profile Headline, Social Links)
           ├── Metrics.jsx (Key Engineering Accomplishments)
           ├── Experience.jsx (Work Timeline at Aspire Systems & Virtusa)
           ├── Skills.jsx (Technical Competency Matrix)
           ├── Projects.jsx (Interactive Cards & SmartStore Flagship)
           ├── ContactForm.jsx (Rate-Limited Contact Handler)
           └── Footer.jsx (Copyright & Tech Stack Note)
```

---

## 2. API Integration & Service Layer (`src/services/api.js`)

Axios is abstracted into a service module (`portfolioApi`):

```javascript
import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5050/api';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: { 'Content-Type': 'application/json' },
  timeout: 10000,
});

export const portfolioApi = {
  getProfile: async () => { ... },
  getExperiences: async () => { ... },
  getSkills: async () => { ... },
  getProjects: async () => { ... },
  submitContactMessage: async (formData) => { ... }
};
```

---

## 3. Data Lifecycle

```text
1. User opens http://localhost:5173
        ↓
2. App.jsx triggers useEffect() hook on mount
        ↓
3. Promise.all dispatches parallel requests:
   - portfolioApi.getProfile()
   - portfolioApi.getExperiences()
   - portfolioApi.getSkills()
   - portfolioApi.getProjects()
        ↓
4. React updates state hooks (setProfile, setExperiences, etc.)
        ↓
5. Loading spinner unmounts; Glassmorphism UI renders smoothly
```

---

## 4. Form Handling & State Management (`ContactForm.jsx`)

* **State:** Local component state handles form inputs (`senderName`, `senderEmail`, `subject`, `message`) and status state (`loading`, `success`, `error`).
* **Validation:** Browser native HTML5 validation paired with backend response validation handling (`HTTP 400` / `HTTP 429`).
* **Feedback:** Displays green checkmark card upon `HTTP 201 Created` or red error banner on failure.
