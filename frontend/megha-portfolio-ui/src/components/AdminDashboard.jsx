import React, { useState, useEffect } from 'react';
import { portfolioApi } from '../services/api';

export default function AdminDashboard({ isOpen, onClose }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [credentials, setCredentials] = useState({ username: '', password: '' });
  const [messages, setMessages] = useState([]);
  const [selectedMessage, setSelectedMessage] = useState(null);
  const [filter, setFilter] = useState('all'); // 'all' | 'unread' | 'read'
  const [searchQuery, setSearchQuery] = useState('');
  const [deleteConfirmId, setDeleteConfirmId] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('adminToken');
    if (token) {
      setIsAuthenticated(true);
      fetchMessages();
    }
  }, [isOpen]);

  const fetchMessages = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await portfolioApi.getContactMessages();
      setMessages(data || []);
    } catch (err) {
      if (err.response?.status === 401 || err.response?.status === 403) {
        setIsAuthenticated(false);
        localStorage.removeItem('adminToken');
        setError("Session expired or unauthorized. Please log in again.");
      } else {
        setError(err.response?.data?.message || "Failed to load contact messages.");
      }
    } finally {
      setLoading(false);
    }
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    try {
      await portfolioApi.login(credentials.username, credentials.password);
      setIsAuthenticated(true);
      setCredentials({ username: '', password: '' });
      await fetchMessages();
    } catch (err) {
      setError(err.response?.data?.message || "Invalid administrator credentials.");
    } finally {
      setLoading(false);
    }
  };

  const handleLogout = () => {
    portfolioApi.logout();
    setIsAuthenticated(false);
    setMessages([]);
    setSelectedMessage(null);
  };

  const handleToggleRead = async (message, e) => {
    if (e) e.stopPropagation();
    try {
      const newStatus = !message.isRead;
      await portfolioApi.updateContactMessageReadStatus(message.id, newStatus);
      setMessages(messages.map(m => m.id === message.id ? { ...m, isRead: newStatus } : m));
      if (selectedMessage?.id === message.id) {
        setSelectedMessage({ ...selectedMessage, isRead: newStatus });
      }
    } catch (err) {
      alert("Failed to update message status.");
    }
  };

  const handleDelete = async (id) => {
    try {
      await portfolioApi.deleteContactMessage(id);
      setMessages(messages.filter(m => m.id !== id));
      if (selectedMessage?.id === id) setSelectedMessage(null);
      setDeleteConfirmId(null);
    } catch (err) {
      alert("Failed to delete contact message.");
    }
  };

  const filteredMessages = messages.filter(m => {
    if (filter === 'unread' && m.isRead) return false;
    if (filter === 'read' && !m.isRead) return false;
    if (searchQuery.trim()) {
      const q = searchQuery.toLowerCase();
      return (
        m.senderName.toLowerCase().includes(q) ||
        m.senderEmail.toLowerCase().includes(q) ||
        m.subject.toLowerCase().includes(q) ||
        m.message.toLowerCase().includes(q)
      );
    }
    return true;
  });

  const unreadCount = messages.filter(m => !m.isRead).length;

  if (!isOpen) return null;

  return (
    <div style={{
      position: 'fixed',
      top: 0,
      left: 0,
      right: 0,
      bottom: 0,
      zIndex: 2000,
      background: 'rgba(11, 15, 23, 0.85)',
      backdropFilter: 'blur(12px)',
      display: 'flex',
      justify: 'center',
      alignItems: 'center',
      padding: '24px'
    }}>
      <div className="glass-card" style={{
        width: '100%',
        maxWidth: '1000px',
        maxHeight: '90vh',
        display: 'flex',
        flexDirection: 'column',
        overflow: 'hidden',
        background: '#0F172A',
        border: '1px solid rgba(59, 130, 246, 0.3)',
        borderRadius: '16px',
        boxShadow: '0 25px 50px -12px rgba(0, 0, 0, 0.7)'
      }}>
        {/* Modal Header */}
        <div style={{
          padding: '20px 24px',
          borderBottom: '1px solid rgba(255, 255, 255, 0.1)',
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          background: 'rgba(15, 23, 42, 0.9)'
        }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
            <span style={{ fontSize: '1.4rem' }}>🛡️</span>
            <div>
              <h3 style={{ fontSize: '1.2rem', fontWeight: 800, margin: 0, color: '#fff' }}>
                Admin Portal — Contact Messages
              </h3>
              <p style={{ fontSize: '0.8rem', color: 'var(--text-secondary)', margin: 0 }}>
                {isAuthenticated ? 'Authenticated Secure Dashboard' : 'Please sign in with administrator credentials'}
              </p>
            </div>
          </div>

          <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
            {isAuthenticated && (
              <>
                <button onClick={fetchMessages} className="btn btn-outline" style={{ padding: '6px 14px', fontSize: '0.8rem' }} title="Refresh Messages">
                  🔄 Refresh
                </button>
                <button onClick={handleLogout} className="btn btn-outline" style={{ padding: '6px 14px', fontSize: '0.8rem', borderColor: 'rgba(239, 68, 68, 0.4)', color: '#EF4444' }}>
                  🚪 Logout
                </button>
              </>
            )}
            <button onClick={onClose} style={{ background: 'none', border: 'none', color: 'var(--text-secondary)', fontSize: '1.5rem', cursor: 'pointer', padding: '4px' }}>
              ✕
            </button>
          </div>
        </div>

        {/* Modal Content */}
        <div style={{ padding: '24px', overflowY: 'auto', flex: 1 }}>
          {!isAuthenticated ? (
            /* Login Form */
            <div style={{ maxWidth: '400px', margin: '40px auto' }}>
              <div style={{ textAlign: 'center', marginBottom: '24px' }}>
                <div style={{ fontSize: '2.5rem', marginBottom: '8px' }}>🔐</div>
                <h4 style={{ fontSize: '1.25rem', fontWeight: 700, color: '#fff' }}>Administrator Authentication</h4>
                <p style={{ fontSize: '0.85rem', color: 'var(--text-secondary)' }}>
                  Sign in with your server-configured administrator credentials.
                </p>
              </div>

              {error && (
                <div style={{ padding: '12px', borderRadius: '8px', background: 'rgba(239, 68, 68, 0.1)', border: '1px solid rgba(239, 68, 68, 0.3)', color: '#EF4444', fontSize: '0.85rem', marginBottom: '16px' }}>
                  ⚠️ {error}
                </div>
              )}

              <form onSubmit={handleLogin} style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
                <div>
                  <label style={{ display: 'block', fontSize: '0.85rem', fontWeight: 600, color: 'var(--text-secondary)', marginBottom: '6px' }}>Username</label>
                  <input
                    type="text"
                    value={credentials.username}
                    onChange={(e) => setCredentials({ ...credentials, username: e.target.value })}
                    required
                    placeholder="Enter admin username"
                    style={{ width: '100%', padding: '10px 14px', borderRadius: '8px', background: 'var(--bg-primary)', border: '1px solid var(--border-color)', color: '#fff', fontSize: '0.9rem' }}
                  />
                </div>
                <div>
                  <label style={{ display: 'block', fontSize: '0.85rem', fontWeight: 600, color: 'var(--text-secondary)', marginBottom: '6px' }}>Password</label>
                  <input
                    type="password"
                    value={credentials.password}
                    onChange={(e) => setCredentials({ ...credentials, password: e.target.value })}
                    required
                    placeholder="Enter admin password"
                    style={{ width: '100%', padding: '10px 14px', borderRadius: '8px', background: 'var(--bg-primary)', border: '1px solid var(--border-color)', color: '#fff', fontSize: '0.9rem' }}
                  />
                </div>
                <button type="submit" disabled={loading} className="btn btn-primary" style={{ padding: '12px', justifyContent: 'center', marginTop: '8px' }}>
                  {loading ? 'Authenticating...' : 'Sign In as Admin ➔'}
                </button>
              </form>
            </div>
          ) : (
            /* Admin Contact Messages Dashboard */
            <div>
              {/* Metrics & Filter Bar */}
              <div style={{ display: 'flex', flexWrap: 'wrap', gap: '16px', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
                <div style={{ display: 'flex', gap: '12px', alignItems: 'center' }}>
                  <span className="badge" style={{ fontSize: '0.85rem', padding: '6px 14px' }}>
                    Total: <strong>{messages.length}</strong>
                  </span>
                  <span className="badge badge-emerald" style={{ fontSize: '0.85rem', padding: '6px 14px' }}>
                    Unread: <strong>{unreadCount}</strong>
                  </span>
                </div>

                <div style={{ display: 'flex', gap: '12px', flexWrap: 'wrap', flex: 1, maxWidth: '500px', justifyContent: 'flex-end' }}>
                  <input
                    type="text"
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                    placeholder="Search name, email, subject..."
                    style={{ padding: '8px 14px', borderRadius: '8px', background: 'var(--bg-primary)', border: '1px solid var(--border-color)', color: '#fff', fontSize: '0.85rem', minWidth: '200px' }}
                  />
                  <div style={{ display: 'inline-flex', borderRadius: '8px', overflow: 'hidden', border: '1px solid var(--border-color)' }}>
                    {['all', 'unread', 'read'].map((f) => (
                      <button
                        key={f}
                        onClick={() => setFilter(f)}
                        style={{
                          padding: '8px 14px',
                          border: 'none',
                          background: filter === f ? 'var(--accent-blue)' : 'var(--bg-primary)',
                          color: filter === f ? '#fff' : 'var(--text-secondary)',
                          fontSize: '0.8rem',
                          fontWeight: 600,
                          cursor: 'pointer',
                          textTransform: 'capitalize'
                        }}>
                        {f}
                      </button>
                    ))}
                  </div>
                </div>
              </div>

              {loading ? (
                <div style={{ padding: '40px', textAlign: 'center', color: 'var(--accent-blue)' }}>
                  Loading contact messages from PostgreSQL database...
                </div>
              ) : filteredMessages.length === 0 ? (
                <div style={{ padding: '40px', textAlign: 'center', color: 'var(--text-secondary)', background: 'rgba(255,255,255,0.02)', borderRadius: '12px', border: '1px border-color' }}>
                  <div style={{ fontSize: '2rem', marginBottom: '8px' }}>📬</div>
                  <p style={{ margin: 0, fontWeight: 600 }}>No contact messages found matching your criteria.</p>
                </div>
              ) : (
                /* Messages Grid List */
                <div style={{ display: 'flex', flexDirection: 'column', gap: '12px' }}>
                  {filteredMessages.map((msg) => (
                    <div
                      key={msg.id}
                      onClick={() => {
                        setSelectedMessage(msg);
                        if (!msg.isRead) handleToggleRead(msg);
                      }}
                      style={{
                        padding: '16px 20px',
                        borderRadius: '12px',
                        background: msg.isRead ? 'rgba(30, 41, 59, 0.4)' : 'rgba(59, 130, 246, 0.08)',
                        border: msg.isRead ? '1px solid rgba(255, 255, 255, 0.08)' : '1px solid rgba(59, 130, 246, 0.3)',
                        cursor: 'pointer',
                        transition: 'all 0.2s ease',
                        display: 'grid',
                        gridTemplateColumns: 'auto 1fr auto',
                        gap: '16px',
                        alignItems: 'center'
                      }}>
                      {/* Status Icon */}
                      <div>
                        <span className={msg.isRead ? "badge badge-emerald" : "badge"} style={{ fontSize: '0.75rem', padding: '4px 10px' }}>
                          {msg.isRead ? 'Read' : 'New'}
                        </span>
                      </div>

                      {/* Content Preview */}
                      <div style={{ overflow: 'hidden' }}>
                        <div style={{ display: 'flex', gap: '12px', alignItems: 'center', marginBottom: '4px' }}>
                          <span style={{ fontWeight: 700, color: '#fff', fontSize: '0.95rem' }}>{msg.senderName}</span>
                          <span style={{ fontSize: '0.8rem', color: 'var(--accent-blue)' }}>&lt;{msg.senderEmail}&gt;</span>
                        </div>
                        <div style={{ fontSize: '0.9rem', fontWeight: 600, color: 'var(--text-primary)', marginBottom: '4px', whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }}>
                          {msg.subject || '(No Subject)'}
                        </div>
                        <div style={{ fontSize: '0.8rem', color: 'var(--text-secondary)', whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }}>
                          {msg.message}
                        </div>
                      </div>

                      {/* Right Actions & Date */}
                      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', gap: '8px' }}>
                        <span style={{ fontSize: '0.75rem', color: 'var(--text-muted)' }}>
                          {new Date(msg.createdAt).toLocaleDateString()} {new Date(msg.createdAt).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
                        </span>
                        <div style={{ display: 'flex', gap: '8px' }}>
                          <button
                            onClick={(e) => handleToggleRead(msg, e)}
                            className="btn btn-outline"
                            style={{ padding: '4px 8px', fontSize: '0.75rem' }}
                            title={msg.isRead ? "Mark as Unread" : "Mark as Read"}>
                            {msg.isRead ? '✉️ Unread' : '✅ Read'}
                          </button>
                          <button
                            onClick={(e) => {
                              e.stopPropagation();
                              setDeleteConfirmId(msg.id);
                            }}
                            className="btn btn-outline"
                            style={{ padding: '4px 8px', fontSize: '0.75rem', borderColor: 'rgba(239, 68, 68, 0.4)', color: '#EF4444' }}
                            title="Delete Message">
                            🗑️
                          </button>
                        </div>
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>
          )}
        </div>
      </div>

      {/* Message Detail Modal / Drawer */}
      {selectedMessage && (
        <div style={{
          position: 'fixed',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          zIndex: 2100,
          background: 'rgba(0, 0, 0, 0.75)',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          padding: '24px'
        }}>
          <div className="glass-card" style={{
            width: '100%',
            maxWidth: '650px',
            background: '#0F172A',
            border: '1px solid var(--accent-blue)',
            padding: '32px',
            borderRadius: '16px',
            boxShadow: '0 25px 50px -12px rgba(0,0,0,0.8)'
          }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: '20px' }}>
              <div>
                <span className={selectedMessage.isRead ? "badge badge-emerald" : "badge"} style={{ marginBottom: '8px' }}>
                  {selectedMessage.isRead ? 'Read Status' : 'Unread Status'}
                </span>
                <h3 style={{ fontSize: '1.4rem', fontWeight: 800, color: '#fff', margin: '4px 0' }}>
                  {selectedMessage.subject || '(No Subject)'}
                </h3>
                <p style={{ color: 'var(--accent-blue)', fontSize: '0.9rem', margin: 0 }}>
                  From: <strong>{selectedMessage.senderName}</strong> &lt;{selectedMessage.senderEmail}&gt;
                </p>
                <p style={{ color: 'var(--text-muted)', fontSize: '0.8rem', marginTop: '4px' }}>
                  Received: {new Date(selectedMessage.createdAt).toLocaleString()}
                </p>
              </div>
              <button onClick={() => setSelectedMessage(null)} style={{ background: 'none', border: 'none', color: 'var(--text-secondary)', fontSize: '1.5rem', cursor: 'pointer' }}>
                ✕
              </button>
            </div>

            <div style={{
              background: 'var(--bg-primary)',
              padding: '20px',
              borderRadius: '8px',
              border: '1px solid var(--border-color)',
              color: 'var(--text-primary)',
              fontSize: '0.95rem',
              lineHeight: 1.7,
              whiteSpace: 'pre-wrap',
              maxHeight: '300px',
              overflowY: 'auto',
              marginBottom: '24px'
            }}>
              {selectedMessage.message}
            </div>

            <div style={{ display: 'flex', gap: '12px', justifyContent: 'flex-end', flexWrap: 'wrap' }}>
              <a
                href={`mailto:${selectedMessage.senderEmail}?subject=Re: ${encodeURIComponent(selectedMessage.subject || 'Portfolio Inquiry')}`}
                className="btn btn-primary"
                style={{ padding: '8px 20px', fontSize: '0.85rem' }}>
                📧 Reply by Email
              </a>
              <button
                onClick={() => handleToggleRead(selectedMessage)}
                className="btn btn-outline"
                style={{ padding: '8px 20px', fontSize: '0.85rem' }}>
                {selectedMessage.isRead ? 'Mark Unread' : 'Mark Read'}
              </button>
              <button
                onClick={() => setDeleteConfirmId(selectedMessage.id)}
                className="btn btn-outline"
                style={{ padding: '8px 20px', fontSize: '0.85rem', borderColor: 'rgba(239, 68, 68, 0.4)', color: '#EF4444' }}>
                🗑️ Delete Message
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Delete Confirmation Alert */}
      {deleteConfirmId && (
        <div style={{
          position: 'fixed',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          zIndex: 2200,
          background: 'rgba(0,0,0,0.8)',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          padding: '24px'
        }}>
          <div className="glass-card" style={{ width: '100%', maxWidth: '420px', background: '#0F172A', padding: '28px', borderRadius: '16px', textAlign: 'center', border: '1px solid rgba(239, 68, 68, 0.4)' }}>
            <div style={{ fontSize: '2.5rem', marginBottom: '12px' }}>⚠️</div>
            <h4 style={{ fontSize: '1.2rem', fontWeight: 700, color: '#fff', marginBottom: '8px' }}>Confirm Message Deletion</h4>
            <p style={{ color: 'var(--text-secondary)', fontSize: '0.9rem', marginBottom: '24px' }}>
              Are you sure you want to permanently delete this contact submission? This action cannot be undone.
            </p>
            <div style={{ display: 'flex', gap: '12px', justifyContent: 'center' }}>
              <button onClick={() => setDeleteConfirmId(null)} className="btn btn-outline" style={{ padding: '8px 20px' }}>
                Cancel
              </button>
              <button onClick={() => handleDelete(deleteConfirmId)} className="btn btn-primary" style={{ padding: '8px 20px', background: '#EF4444', borderColor: '#EF4444' }}>
                Yes, Delete
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
