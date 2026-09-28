import React, { useState } from 'react';
import { portfolioApi } from '../services/api';

export default function ContactForm() {
  const [formData, setFormData] = useState({
    senderName: '',
    senderEmail: '',
    subject: '',
    message: ''
  });
  const [status, setStatus] = useState({ loading: false, success: false, error: null });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setStatus({ loading: true, success: false, error: null });

    try {
      await portfolioApi.submitContactMessage(formData);
      setStatus({ loading: false, success: true, error: null });
      setFormData({ senderName: '', senderEmail: '', subject: '', message: '' });
    } catch (err) {
      let errorMsg = 'Unable to send your message. Please try again.';
      if (err.code === 'ECONNABORTED') {
        errorMsg = 'The request took too long to reach the server. Please try again.';
      } else if (err.response?.data?.message) {
        errorMsg = err.response.data.message;
      }
      setStatus({ loading: false, success: false, error: errorMsg });
    }
  };

  return (
    <section id="contact" className="section" style={{ background: 'var(--bg-secondary)' }}>
      <div className="container" style={{ maxWidth: '800px' }}>
        <h2 className="section-title" style={{ textAlign: 'center' }}>Get In Touch</h2>
        <p className="section-subtitle" style={{ textAlign: 'center', margin: '0 auto 48px auto' }}>
          Have a senior .NET engineering role or technical inquiry? Send a direct message below.
        </p>

        <div className="glass-card" style={{ padding: '40px' }}>
          {status.success ? (
            <div style={{ textAlign: 'center', padding: '32px' }}>
              <div style={{ fontSize: '3rem', marginBottom: '16px' }}>✅</div>
              <h3 style={{ fontSize: '1.5rem', fontWeight: 700, color: 'var(--accent-emerald)', marginBottom: '8px' }}>
                Message Received!
              </h3>
              <p style={{ color: 'var(--text-secondary)' }}>
                Thank you for reaching out. I will get back to you promptly.
              </p>
              <button 
                onClick={() => setStatus({ loading: false, success: false, error: null })} 
                className="btn btn-primary" 
                style={{ marginTop: '24px' }}>
                Send Another Message
              </button>
            </div>
          ) : (
            <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
              {status.error && (
                <div style={{ padding: '12px 16px', borderRadius: '8px', background: 'rgba(239, 68, 68, 0.1)', border: '1px solid rgba(239, 68, 68, 0.3)', color: '#EF4444', fontSize: '0.9rem' }}>
                  ⚠️ {status.error}
                </div>
              )}

              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(280px, 1fr))', gap: '20px' }}>
                <div>
                  <label style={{ display: 'block', fontSize: '0.85rem', fontWeight: 600, color: 'var(--text-secondary)', marginBottom: '8px' }}>Your Name *</label>
                  <input
                    type="text"
                    name="senderName"
                    value={formData.senderName}
                    onChange={handleChange}
                    required
                    autoComplete="name"
                    placeholder="e.g. Technical Recruiter"
                    style={{ width: '100%', padding: '12px 16px', borderRadius: '8px', background: 'var(--bg-primary)', border: '1px solid var(--border-color)', color: '#fff', fontSize: '0.95rem' }}
                  />
                </div>
                <div>
                  <label style={{ display: 'block', fontSize: '0.85rem', fontWeight: 600, color: 'var(--text-secondary)', marginBottom: '8px' }}>Your Email *</label>
                  <input
                    type="email"
                    name="senderEmail"
                    value={formData.senderEmail}
                    onChange={handleChange}
                    required
                    autoComplete="email"
                    inputMode="email"
                    placeholder="e.g. recruiter@company.com"
                    style={{ width: '100%', padding: '12px 16px', borderRadius: '8px', background: 'var(--bg-primary)', border: '1px solid var(--border-color)', color: '#fff', fontSize: '0.95rem' }}
                  />
                </div>
              </div>

              <div>
                <label style={{ display: 'block', fontSize: '0.85rem', fontWeight: 600, color: 'var(--text-secondary)', marginBottom: '8px' }}>Subject</label>
                <input
                  type="text"
                  name="subject"
                  value={formData.subject}
                  onChange={handleChange}
                  placeholder="e.g. Senior .NET Opportunity"
                  style={{ width: '100%', padding: '12px 16px', borderRadius: '8px', background: 'var(--bg-primary)', border: '1px solid var(--border-color)', color: '#fff', fontSize: '0.95rem' }}
                />
              </div>

              <div>
                <label style={{ display: 'block', fontSize: '0.85rem', fontWeight: 600, color: 'var(--text-secondary)', marginBottom: '8px' }}>Message *</label>
                <textarea
                  name="message"
                  value={formData.message}
                  onChange={handleChange}
                  required
                  rows={5}
                  placeholder="Write your message here..."
                  style={{ width: '100%', padding: '12px 16px', borderRadius: '8px', background: 'var(--bg-primary)', border: '1px solid var(--border-color)', color: '#fff', fontSize: '0.95rem', resize: 'vertical' }}
                />
              </div>

              <button type="submit" disabled={status.loading} className="btn btn-primary" style={{ justifySelf: 'flex-start', padding: '14px 32px' }}>
                {status.loading ? 'Submitting Message...' : 'Send Message ✉️'}
              </button>
            </form>
          )}
        </div>
      </div>
    </section>
  );
}
