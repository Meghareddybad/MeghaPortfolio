import React from 'react';

export default function Skills({ skills }) {
  return (
    <section id="skills" className="section" style={{ background: 'var(--bg-secondary)' }}>
      <div className="container">
        <h2 className="section-title">Technical Competency & Architecture</h2>
        <p className="section-subtitle">
          Core technical domain expertise matching enterprise engineering standards.
        </p>

        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(320px, 1fr))', gap: '24px' }}>
          {skills.map((s) => (
            <div key={s.id} className="glass-card" style={{ padding: '28px' }}>
              <h3 style={{ fontSize: '1.15rem', fontWeight: 700, color: 'var(--accent-blue)', marginBottom: '12px' }}>
                {s.category}
              </h3>
              <p style={{ color: 'var(--text-secondary)', fontSize: '0.95rem', lineHeight: 1.7 }}>
                {s.skillList}
              </p>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
