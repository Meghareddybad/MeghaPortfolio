import React from 'react';

export default function Hero({ profile }) {
  return (
    <section id="hero" style={{ paddingTop: '160px', paddingBottom: '96px', position: 'relative' }}>
      <div className="container" style={{ display: 'grid', gridTemplateColumns: '1fr', gap: '48px' }}>
        <div>
          <div style={{ display: 'inline-flex', alignItems: 'center', gap: '8px', padding: '6px 16px', borderRadius: '9999px', background: 'rgba(59, 130, 246, 0.1)', border: '1px solid rgba(59, 130, 246, 0.3)', marginBottom: '24px' }}>
            <span style={{ width: '8px', height: '8px', borderRadius: '50%', background: 'var(--accent-emerald)' }}></span>
            <span style={{ fontSize: '0.85rem', fontWeight: 600, color: 'var(--accent-blue)' }}>Available for Senior .NET & Full-Stack Opportunities</span>
          </div>
          
          <h1 style={{ fontSize: '3.5rem', fontWeight: 800, lineHeight: 1.15, marginBottom: '20px', letterSpacing: '-0.03em' }}>
            {profile?.fullName || "Megha Syam Reddy Badhuri"}
          </h1>
          
          <p style={{ fontSize: '1.5rem', color: 'var(--accent-blue)', fontWeight: 600, marginBottom: '20px' }}>
            {profile?.title || "Senior .NET & Backend Engineer"}
          </p>

          <p style={{ fontSize: '1.15rem', color: 'var(--text-secondary)', maxWidth: '720px', lineHeight: 1.7, marginBottom: '40px' }}>
            Architecting high-performance microservices, RESTful APIs, and concurrent backend systems in <strong style={{ color: '#fff' }}>C# .NET 9</strong> and <strong style={{ color: '#fff' }}>React</strong>. Delivering enterprise reliability with 95% latency reduction via TPL concurrency and 85%+ NUnit test coverage.
          </p>

          <div style={{ display: 'flex', gap: '16px', flexWrap: 'wrap', alignItems: 'center' }}>
            <a href="#projects" className="btn btn-primary">
              View Flagship Projects ➔
            </a>
            <a href={profile?.linkedInUrl || "https://www.linkedin.com/in/megha-syam-reddy-badhuri-79531914b"} target="_blank" rel="noreferrer" className="btn btn-outline">
              LinkedIn Profile 🔗
            </a>
            <a href={profile?.gitHubUrl || "https://github.com/meghasyamreddy"} target="_blank" rel="noreferrer" className="btn btn-outline">
              GitHub 💻
            </a>
          </div>
        </div>
      </div>
    </section>
  );
}
