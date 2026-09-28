using MeghaPortfolio.API.Core.Domain.Entities;

namespace MeghaPortfolio.API.Infrastructure.Persistence.Data;

public static class PortfolioDataSeeder
{
    public static void SeedData(PortfolioDbContext context)
    {
        // Ensure Database is Created
        context.Database.EnsureCreated();

        // 1. Seed or Update Profile
        var existingProfile = context.Profiles.FirstOrDefault();
        if (existingProfile == null)
        {
            context.Profiles.Add(new ProfileEntity
            {
                FullName = "Megha Syam Reddy Badhuri",
                Title = "Senior .NET & Backend Engineer",
                Summary = "Results-driven Senior .NET & Backend Engineer with nearly 5 years of experience designing, developing, and scaling high-performance microservices, RESTful APIs, and distributed backend systems. Proficient in C#, .NET Core, Web API, .NET Framework, Multithreading (TPL), SQL Server, PostgreSQL, and MongoDB (NoSQL). Strong background in SOLID principles, Design Patterns, TDD with NUnit, and Kafka event streaming.",
                Location = "Hyderabad, India",
                Email = "meghasyamreddy.dev@gmail.com",
                Phone = "+91 9491364416",
                LinkedInUrl = "https://www.linkedin.com/in/megha-syam-reddy-badhuri-79531914b",
                GitHubUrl = "https://github.com/Meghareddybad/MeghaPortfolio",
                YearsOfExperience = 5,
                LatencyReductionMetric = "95% Processing Latency Reduction via TPL Concurrency",
                TestCoverageMetric = "85%+ Automated Unit Test Coverage (NUnit/TDD)"
            });
        }
        else
        {
            // Update email & GitHub URL to exact repository link
            existingProfile.Email = "meghasyamreddy.dev@gmail.com";
            existingProfile.GitHubUrl = "https://github.com/Meghareddybad/MeghaPortfolio";
        }

        // 2. Seed Experience if empty
        if (!context.Experiences.Any())
        {
            context.Experiences.AddRange(
                new ExperienceEntity
                {
                    Company = "Aspire Systems",
                    Role = "Senior Integration Engineer",
                    DateRange = "Jun 2025 – Jun 2026",
                    IsCurrentRole = false,
                    KeyHighlight = "Achieved a 95% reduction in processing latency for high-throughput data pipelines using C# Task Parallel Library (TPL).",
                    Responsibilities = new List<string>
                    {
                        "Re-architected backend microservices and data orchestration pipelines using .NET Core and Parallel Programming (TPL), achieving a 95% reduction in processing latency.",
                        "Participated in system modernization initiatives exploring event-driven patterns with Apache Kafka for real-time data streaming.",
                        "Engineered high-throughput database interactions across SQL Server and MongoDB (NoSQL) collections with structured logging and automated failover.",
                        "Enforced strict adherence to SOLID principles and industry standard Design Patterns during peer code reviews."
                    },
                    Technologies = new List<string> { "C#", ".NET Core", "TPL Multithreading", "SQL Server", "MongoDB", "Apache Kafka", "AWS S3", "Azure" }
                },
                new ExperienceEntity
                {
                    Company = "Virtusa",
                    Role = "Engineer (.NET Developer)",
                    DateRange = "Aug 2021 – May 2025",
                    IsCurrentRole = false,
                    KeyHighlight = "Spearheaded unit testing strategies using NUnit & TDD, maintaining over 85% test coverage across core business services.",
                    Responsibilities = new List<string>
                    {
                        "Designed, developed, and deployed resilient RESTful Web APIs and microservices using .NET Core, .NET Framework, and C#.",
                        "Spearheaded unit testing strategies using NUnit, crafting comprehensive automated test suites to validate business logic with 85%+ coverage.",
                        "Implemented asynchronous processing utilizing Multithreading and Task Parallel Library (TPL) to optimize concurrent API requests.",
                        "Developed data access layers utilizing Entity Framework Core and native drivers for relational databases and MongoDB.",
                        "Built dynamic, responsive UI modules using React.js, integrating seamlessly with underlying ASP.NET Core endpoints."
                    },
                    Technologies = new List<string> { "C#", ".NET Core", "ASP.NET Web API", "EF Core", "NUnit", "TDD", "React.js", "SQL Server", "GitHub" }
                }
            );
        }

        // 3. Seed Technical Skills if empty
        if (!context.Skills.Any())
        {
            context.Skills.AddRange(
                new SkillEntity { Category = "Backend Development", SkillList = "C#, .NET 9 / .NET Core, ASP.NET Web API, .NET Framework, Microservices, Multithreading & TPL, Entity Framework Core, LINQ", DisplayOrder = 1 },
                new SkillEntity { Category = "Databases & Storage", SkillList = "PostgreSQL, SQL Server, MongoDB (NoSQL), Amazon Redshift, Redis Caching", DisplayOrder = 2 },
                new SkillEntity { Category = "Architecture & Patterns", SkillList = "SOLID Principles, Clean Architecture, Design Patterns (Repository, Factory, Singleton, DI), REST API Design", DisplayOrder = 3 },
                new SkillEntity { Category = "Testing & Quality Assurance", SkillList = "NUnit, Test-Driven Development (TDD), Unit Testing, Moq Framework, EF Core In-Memory", DisplayOrder = 4 },
                new SkillEntity { Category = "Messaging & Cloud", SkillList = "Apache Kafka, Microsoft Azure, AWS S3, YARP Reverse Proxy, Docker Containers", DisplayOrder = 5 },
                new SkillEntity { Category = "DevOps & Tools", SkillList = "GitHub, Git, Swagger / OpenAPI, Postman, CI/CD Pipelines, Jira, Agile / Scrum", DisplayOrder = 6 },
                new SkillEntity { Category = "Frontend & Scripting", SkillList = "React.js, JavaScript (ES6+), HTML5, CSS3, Python", DisplayOrder = 7 }
            );
        }

        // 4. Seed Projects if empty
        if (!context.Projects.Any())
        {
            context.Projects.AddRange(
                new ProjectEntity
                {
                    Title = "SmartStore — Enterprise Microservices Platform",
                    Tagline = "Clean Architecture Fashion E-Commerce Platform in .NET 9 with Polyglot Persistence & Kafka",
                    Architecture = "Clean Architecture / Decoupled Microservices with YARP API Gateway",
                    Description = "Enterprise e-commerce platform exposing microservices (Auth, Product Catalog, Order Processing, Notifications) behind YARP API Gateway. Features polyglot persistence (SQL Server for Orders/Identity + MongoDB for Dynamic Catalog), Kafka event streaming for OrderPlaced events, server-side binary magic byte file upload validation, and React SPAs.",
                    Technologies = new List<string> { "C#", ".NET 9", "ASP.NET Core Web API", "YARP Gateway", "SQL Server", "MongoDB", "Apache Kafka", "JWT Auth", "NUnit", "React.js" },
                    KeyContribution = "Architected multi-service solution with YARP reverse proxy routing, polyglot persistence, Kafka event streaming, and binary magic byte security.",
                    MeasurableResult = "100% NUnit test suite pass rate, zero-downtime EF Core in-memory dev fallback, sub-100ms API response time.",
                    IsFlagship = true,
                    GitHubUrl = "https://github.com/Meghareddybad/MeghaPortfolio",
                    LiveDemoUrl = null
                },
                new ProjectEntity
                {
                    Title = "Enterprise Admin & Workflow Platform",
                    Tagline = "Microservices Administrative Portal with Role-Based Access Control & Batch Processing",
                    Architecture = "Microservices Architecture / Clean Architecture",
                    Description = "Designed and implemented a microservices-based administrative portal handling role-based access control (RBAC), multi-threaded batch operations, and decoupled service endpoints adhering to SOLID standards.",
                    Technologies = new List<string> { "C#", ".NET Core", "Web API", "EF Core", "SQL Server", "React.js", "TPL Multithreading" },
                    KeyContribution = "Engineered RBAC security layer and multi-threaded batch processing workflows using TPL.",
                    MeasurableResult = "Streamlined enterprise admin operations and decoupled service integrations.",
                    IsFlagship = false,
                    GitHubUrl = null,
                    LiveDemoUrl = null
                },
                new ProjectEntity
                {
                    Title = "GenAI Conversational Chatbot (Hackathon Finalist)",
                    Tagline = "Intelligent RAG Chatbot leveraging Vector DBs & LLM APIs (Top Team out of 50+)",
                    Architecture = "RAG (Retrieval-Augmented Generation) Architecture",
                    Description = "Architected an intelligent RAG-based AI Conversational Chatbot leveraging Vector Databases, LLM APIs, Streamlit, and Python to query structured and unstructured enterprise documents with high precision.",
                    Technologies = new List<string> { "Python", "Streamlit", "Vector DB (Chroma/FAISS)", "LLM APIs", "RAG Architecture" },
                    KeyContribution = "Designed vector embedding document retrieval pipeline and prompt engineering orchestration.",
                    MeasurableResult = "Top Finalist among 50+ competing teams at GenAI Hackathon.",
                    IsFlagship = false,
                    GitHubUrl = null,
                    LiveDemoUrl = null
                }
            );
        }

        context.SaveChanges();
    }
}
