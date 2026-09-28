import axios from 'axios';

// Base API configuration pointing to production ASP.NET Core 9 Web API on Render
const API_BASE_URL = import.meta.env.VITE_API_URL || 'https://megha-portfolio-api-hbb8.onrender.com/api';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000,
});

export const portfolioApi = {
  getProfile: async () => {
    try {
      const response = await apiClient.get('/profile');
      return response.data;
    } catch (error) {
      console.warn('Backend API offline, serving fallback profile data', error);
      return {
        fullName: "Megha Syam Reddy Badhuri",
        title: "Senior .NET & Backend Engineer",
        summary: "Results-driven Senior .NET & Backend Engineer with nearly 5 years of experience designing, developing, and scaling high-performance microservices, RESTful APIs, and distributed backend systems. Proficient in C#, .NET Core, Web API, .NET Framework, Multithreading (TPL), SQL Server, PostgreSQL, and MongoDB (NoSQL).",
        location: "Hyderabad, India",
        email: "meghasyamreddy7@gmail.com",
        phone: "+91 9491364416",
        linkedInUrl: "https://www.linkedin.com/in/megha-syam-reddy-badhuri-79531914b",
        gitHubUrl: "https://github.com/meghasyamreddy",
        yearsOfExperience: 5,
        latencyReductionMetric: "95% Processing Latency Reduction via TPL Concurrency",
        testCoverageMetric: "85%+ Automated Unit Test Coverage (NUnit/TDD)"
      };
    }
  },

  getExperiences: async () => {
    try {
      const response = await apiClient.get('/experience');
      return response.data;
    } catch (error) {
      return [
        {
          id: 1,
          company: "Aspire Systems",
          role: "Senior Integration Engineer",
          dateRange: "Jun 2025 – Jun 2026",
          keyHighlight: "Achieved 95% latency reduction in data pipelines using C# TPL Multithreading.",
          responsibilities: [
            "Re-architected backend microservices using .NET Core & TPL, achieving 95% processing latency reduction.",
            "Modernized systems with Apache Kafka event-driven real-time streaming.",
            "Engineered high-throughput DB interactions across SQL Server and MongoDB."
          ],
          technologies: ["C#", ".NET Core", "TPL Multithreading", "SQL Server", "MongoDB", "Apache Kafka"]
        },
        {
          id: 2,
          company: "Virtusa",
          role: "Engineer (.NET Developer)",
          dateRange: "Aug 2021 – May 2025",
          keyHighlight: "Spearheaded NUnit unit testing strategies maintaining 85%+ test coverage.",
          responsibilities: [
            "Designed and deployed RESTful Web APIs and microservices using .NET Core & C#.",
            "Built automated test suites using NUnit & TDD maintaining 85%+ code coverage.",
            "Developed dynamic UI modules in React.js integrated with ASP.NET Core endpoints."
          ],
          technologies: ["C#", ".NET Core", "Web API", "NUnit", "TDD", "React.js", "SQL Server"]
        }
      ];
    }
  },

  getSkills: async () => {
    try {
      const response = await apiClient.get('/skills');
      return response.data;
    } catch (error) {
      return [
        { id: 1, category: "Backend Development", skillList: "C#, .NET 9 / .NET Core, ASP.NET Web API, Microservices, Multithreading & TPL, EF Core, LINQ" },
        { id: 2, category: "Databases & Storage", skillList: "PostgreSQL, SQL Server, MongoDB (NoSQL), Amazon Redshift, Redis" },
        { id: 3, category: "Architecture", skillList: "SOLID Principles, Clean Architecture, Repository Pattern, Dependency Injection, REST APIs" },
        { id: 4, category: "Testing & QA", skillList: "NUnit, Test-Driven Development (TDD), Unit Testing, Moq Framework" },
        { id: 5, category: "Cloud & Messaging", skillList: "Apache Kafka, Microsoft Azure, AWS S3, YARP Reverse Proxy, Docker" }
      ];
    }
  },

  getProjects: async () => {
    try {
      const response = await apiClient.get('/projects');
      return response.data;
    } catch (error) {
      return [
        {
          id: 1,
          title: "SmartStore — Enterprise Microservices Platform",
          tagline: "Clean Architecture Fashion E-Commerce Platform in .NET 9 with Polyglot Persistence & Kafka",
          architecture: "Clean Architecture / Decoupled Microservices with YARP API Gateway",
          description: "Enterprise e-commerce platform exposing microservices (Auth, Product Catalog, Order Processing, Notifications) behind YARP API Gateway. Features polyglot persistence (SQL Server + MongoDB), Kafka event streaming, and server-side binary magic byte security.",
          technologies: ["C#", ".NET 9", "YARP Gateway", "SQL Server", "MongoDB", "Apache Kafka", "React.js"],
          keyContribution: "Architected multi-service solution with YARP gateway, polyglot persistence, and binary magic byte upload security.",
          measurableResult: "100% NUnit test pass rate, sub-100ms API response time.",
          isFlagship: true,
          gitHubUrl: "https://github.com/meghasyamreddy/SmartStore"
        }
      ];
    }
  },

  submitContactMessage: async (formData) => {
    const response = await apiClient.post('/contact', formData);
    return response.data;
  }
};
