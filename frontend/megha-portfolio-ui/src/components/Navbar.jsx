import React, { useState } from 'react';

export default function Navbar() {
  const [scrolled, setScrolled] = useState(false);

  React.useEffect(() => {
    const handleScroll = () => {
      setScrolled(window.scrollY > 50);
    };
    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  return (
    <nav style={{
      position: 'fixed',
      top: 0,
      left: 0,
      right: 0,
      zIndex: 1000,
      padding: '20px 0',
      background: scrolled ? 'rgba(11, 15, 23, 0.9)' : 'transparent',
      backdropFilter: scrolled ? 'blur(16px)' : 'none',
      borderBottom: scrolled ? '1px solid rgba(255, 255, 255, 0.1)' : 'none',
      transition: 'all 0.3s ease'
    }}>
      <div className="container" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <a href="#hero" style={{ textDecoration: 'none', color: '#fff', fontSize: '1.25rem', fontWeight: 800 }}>
          MEGHA<span style={{ color: 'var(--accent-blue)' }}>.DEV</span>
        </a>
        <div style={{ display: 'flex', gap: '32px', alignItems: 'center' }}>
          <a href="#about" style={{ color: 'var(--text-secondary)', textDecoration: 'none', fontWeight: 500 }}>About</a>
          <a href="#experience" style={{ color: 'var(--text-secondary)', textDecoration: 'none', fontWeight: 500 }}>Experience</a>
          <a href="#skills" style={{ color: 'var(--text-secondary)', textDecoration: 'none', fontWeight: 500 }}>Skills</a>
          <a href="#projects" style={{ color: 'var(--text-secondary)', textDecoration: 'none', fontWeight: 500 }}>Projects</a>
          <a href="#contact" className="btn btn-primary" style={{ padding: '8px 20px', fontSize: '0.85rem' }}>Contact Me</a>
        </div>
      </div>
    </nav>
  );
}
