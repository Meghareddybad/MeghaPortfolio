import React from 'react';

export default function Metrics({ profile }) {
  const metrics = [
    { label: "Years of Experience", value: "nearly 5 YOE", subtext: "Aspire Systems & Virtusa" },
    { label: "Latency Reduction", value: "95% Reduced", subtext: "TPL Parallel Programming" },
    { label: "Unit Test Coverage", value: "85%+ Coverage", subtext: "NUnit & TDD Strategy" },
    { label: "Hackathon Standing", value: "Top Finalist", subtext: "GenAI Chatbot out of 50+ teams" }
  ];

  return (
    <section style={{ padding: '0 0 64px 0' }}>
      <div className="container">
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(240px, 1fr))', gap: '20px' }}>
          {metrics.map((m, idx) => (
            <div key={idx} className="glass-card" style={{ padding: '28px' }}>
              <div style={{ fontSize: '0.85rem', color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '1px', fontWeight: 700, marginBottom: '8px' }}>
                {m.label}
              </div>
              <div style={{ fontSize: '1.85rem', fontWeight: 800, color: 'var(--accent-blue)', marginBottom: '4px' }}>
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
