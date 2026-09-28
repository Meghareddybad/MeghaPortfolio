import React from 'react';

export default function About({ profile }) {
  const metrics = [
    { label: "Years of Experience", value: "Nearly 5 YOE", subtext: "Aspire Systems & Virtusa" },
    { label: "Latency Reduction", value: "95% Reduced", subtext: "TPL Parallel Programming" },
    { label: "Unit Test Coverage", value: "85%+ Coverage", subtext: "NUnit & TDD Strategy" },
    { label: "Hackathon Standing", value: "Top Finalist", subtext: "GenAI Chatbot out of 50+ teams" }
  ];

  return (
    <section id="about" style={{ paddingTop: '80px', paddingBottom: '80px', position: 'relative' }}>
      <div className="container">
        <div style={{ marginBottom: '48px', textAlign: 'center' }}>
          <h2 style={{ fontSize: '2.25rem', fontWeight: 800, marginBottom: '12px' }}>
            About <span style={{ color: 'var(--accent-blue)' }}>Me</span>
          </h2>
          <p style={{ color: 'var(--text-secondary)', fontSize: '1.1rem', maxWidth: '720px', margin: '0 auto', lineHeight: 1.6 }}>
            Senior Backend & Full-Stack Engineer with nearly 5 years of enterprise experience building robust RESTful APIs, distributed microservices, and modern web applications.
          </p>
        </div>

        <div className="glass-card" style={{ padding: '36px', marginBottom: '40px', background: 'rgba(15, 23, 42, 0.6)', border: '1px solid rgba(255, 255, 255, 0.08)' }}>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '32px' }}>
            <div>
              <h3 style={{ fontSize: '1.3rem', fontWeight: 700, color: '#fff', marginBottom: '16px', display: 'flex', alignItems: 'center', gap: '10px' }}>
                <span style={{ color: 'var(--accent-blue)' }}>🚀</span> Professional Journey
              </h3>
              <p style={{ color: 'var(--text-secondary)', lineHeight: 1.7, fontSize: '0.98rem' }}>
                Having worked with top-tier technology consulting firms like <strong style={{ color: '#fff' }}>Aspire Systems</strong> and <strong style={{ color: '#fff' }}>Virtusa</strong>, I specialize in designing scalable backend architectures using <strong style={{ color: '#fff' }}>C# .NET 9</strong>, Entity Framework Core, PostgreSQL, and React.
              </p>
            </div>
            <div>
              <h3 style={{ fontSize: '1.3rem', fontWeight: 700, color: '#fff', marginBottom: '16px', display: 'flex', alignItems: 'center', gap: '10px' }}>
                <span style={{ color: 'var(--accent-emerald)' }}>💡</span> Engineering Philosophy
              </h3>
              <p style={{ color: 'var(--text-secondary)', lineHeight: 1.7, fontSize: '0.98rem' }}>
                I prioritize clean code, test-driven development (TDD), and performance optimization. My systems emphasize high concurrency, resilient rate limiting, secure JWT authentication, and zero-downtime deployment.
              </p>
            </div>
          </div>
        </div>

        {/* Key Highlight Metrics */}
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))', gap: '20px' }}>
          {metrics.map((m, idx) => (
            <div key={idx} className="glass-card" style={{ padding: '24px', textAlign: 'center' }}>
              <div style={{ fontSize: '0.8rem', color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '1px', fontWeight: 700, marginBottom: '8px' }}>
                {m.label}
              </div>
              <div style={{ fontSize: '1.75rem', fontWeight: 800, color: 'var(--accent-blue)', marginBottom: '4px' }}>
                {m.value}
              </div>
              <div style={{ fontSize: '0.85rem', color: 'var(--text-secondary)' }}>
                {m.subtext}
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
