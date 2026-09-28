import React from 'react';

export default function Projects({ projects }) {
  return (
    <section id="projects" className="section">
      <div className="container">
        <h2 className="section-title">Featured Projects</h2>
        <p className="section-subtitle">
          Real-world architectural showcases demonstrating polyglot persistence, microservices, and GenAI integrations.
        </p>

        <div style={{ display: 'flex', flexDirection: 'column', gap: '36px' }}>
          {projects.map((proj) => (
            <div key={proj.id} className="glass-card" style={{ padding: '40px', border: proj.isFlagship ? '1px solid var(--accent-blue)' : '1px solid var(--border-color)' }}>
              {proj.isFlagship && (
                <div style={{ marginBottom: '16px' }}>
                  <span className="badge badge-emerald">★ FLAGSHIP PORTFOLIO PROJECT</span>
                </div>
              )}
              
              <h3 style={{ fontSize: '1.75rem', fontWeight: 800, color: '#fff', marginBottom: '8px' }}>
                {proj.title}
              </h3>
              
              <div style={{ fontSize: '1rem', color: 'var(--accent-blue)', fontWeight: 600, marginBottom: '20px' }}>
                {proj.tagline || proj.architecture}
              </div>

              <p style={{ color: 'var(--text-secondary)', fontSize: '1rem', lineHeight: 1.7, marginBottom: '24px' }}>
                {proj.description}
              </p>

              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(280px, 1fr))', gap: '16px', background: 'rgba(0,0,0,0.2)', padding: '20px', borderRadius: '12px', marginBottom: '24px' }}>
                <div>
                  <div style={{ fontSize: '0.8rem', fontWeight: 700, color: 'var(--text-muted)', uppercase: true, marginBottom: '4px' }}>KEY CONTRIBUTION</div>
                  <div style={{ fontSize: '0.9rem', color: '#E2E8F0' }}>{proj.keyContribution}</div>
                </div>
                <div>
                  <div style={{ fontSize: '0.8rem', fontWeight: 700, color: 'var(--text-muted)', uppercase: true, marginBottom: '4px' }}>MEASURABLE RESULT</div>
                  <div style={{ fontSize: '0.9rem', color: 'var(--accent-emerald)', fontWeight: 600 }}>{proj.measurableResult}</div>
                </div>
              </div>

              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', flexWrap: 'wrap', gap: '16px' }}>
                <div style={{ display: 'flex', gap: '8px', flexWrap: 'wrap' }}>
                  {proj.technologies.map((tech, i) => (
                    <span key={i} className="badge" style={{ fontSize: '0.75rem' }}>{tech}</span>
                  ))}
                </div>
                
                {proj.gitHubUrl && (
                  <a href={proj.gitHubUrl} target="_blank" rel="noreferrer" className="btn btn-outline" style={{ fontSize: '0.85rem' }}>
                    View GitHub Repository 💻
                  </a>
                )}
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
