import React from 'react';

export default function Experience({ experiences }) {
  return (
    <section id="experience" className="section">
      <div className="container">
        <h2 className="section-title">Professional Experience</h2>
        <p className="section-subtitle">
          Track record of engineering high-throughput microservices, parallel processing pipelines, and resilient APIs.
        </p>

        <div style={{ display: 'flex', flexDirection: 'column', gap: '32px' }}>
          {experiences.map((exp) => (
            <div key={exp.id} className="glass-card" style={{ padding: '36px' }}>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', flexWrap: 'wrap', gap: '12px', marginBottom: '16px' }}>
                <div>
                  <h3 style={{ fontSize: '1.4rem', fontWeight: 700, color: '#fff' }}>{exp.company}</h3>
                  <div style={{ fontSize: '1.05rem', color: 'var(--accent-blue)', fontWeight: 600 }}>{exp.role}</div>
                </div>
                <span className="badge">{exp.dateRange}</span>
              </div>

              {exp.keyHighlight && (
                <div style={{ padding: '12px 16px', borderRadius: '8px', background: 'rgba(16, 185, 129, 0.1)', borderLeft: '4px solid var(--accent-emerald)', marginBottom: '20px', fontSize: '0.95rem', color: '#E2E8F0' }}>
                  <strong>Key Highlight:</strong> {exp.keyHighlight}
                </div>
              )}

              <ul style={{ listStyle: 'none', padding: 0, marginBottom: '24px' }}>
                {exp.responsibilities.map((res, i) => (
                  <li key={i} style={{ position: 'relative', paddingLeft: '24px', marginBottom: '10px', color: 'var(--text-secondary)', fontSize: '0.95rem' }}>
                    <span style={{ position: 'absolute', left: 0, color: 'var(--accent-blue)' }}>▸</span>
                    {res}
                  </li>
                ))}
              </ul>

              <div style={{ display: 'flex', gap: '8px', flexWrap: 'wrap' }}>
                {exp.technologies.map((tech, i) => (
                  <span key={i} style={{ fontSize: '0.75rem', fontWeight: 600, padding: '4px 10px', borderRadius: '4px', background: 'rgba(255, 255, 255, 0.05)', color: 'var(--text-secondary)', border: '1px solid rgba(255, 255, 255, 0.08)' }}>
                    {tech}
                  </span>
                ))}
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
