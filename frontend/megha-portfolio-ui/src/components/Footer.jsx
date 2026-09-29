import React from 'react';

export default function Footer({ onOpenAdmin }) {
  return (
    <footer style={{ padding: '48px 0', background: 'var(--bg-primary)', borderTop: '1px solid var(--border-color)', textAlign: 'center', color: 'var(--text-muted)', fontSize: '0.9rem' }}>
      <div className="container">
        <p style={{ marginBottom: '8px' }}>
          Built with <strong style={{ color: '#fff' }}>ASP.NET Core 9 Web API</strong> + <strong style={{ color: '#fff' }}>React JavaScript</strong> + <strong style={{ color: '#fff' }}>PostgreSQL</strong>.
        </p>
        <p style={{ marginBottom: '8px' }}>
          © {new Date().getFullYear()} Megha Syam Reddy Badhuri. All rights reserved.
        </p>
        {onOpenAdmin && (
          <p style={{ margin: 0, fontSize: '0.8rem' }}>
            <button onClick={onOpenAdmin} style={{ background: 'none', border: 'none', color: 'var(--text-muted)', textDecoration: 'underline', cursor: 'pointer' }}>
              🔒 Owner / Admin Messages Portal
            </button>
          </p>
        )}
      </div>
    </footer>
  );
}
