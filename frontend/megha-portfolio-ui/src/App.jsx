import React, { useState, useEffect } from 'react';
import Navbar from './components/Navbar';
import Hero from './components/Hero';
import About from './components/About';
import Experience from './components/Experience';
import Skills from './components/Skills';
import Projects from './components/Projects';
import ContactForm from './components/ContactForm';
import Footer from './components/Footer';
import { portfolioApi } from './services/api';

export default function App() {
  const [profile, setProfile] = useState(null);
  const [experiences, setExperiences] = useState([]);
  const [skills, setSkills] = useState([]);
  const [projects, setProjects] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [profileData, expData, skillData, projectData] = await Promise.all([
          portfolioApi.getProfile(),
          portfolioApi.getExperiences(),
          portfolioApi.getSkills(),
          portfolioApi.getProjects()
        ]);

        setProfile(profileData);
        setExperiences(expData);
        setSkills(skillData);
        setProjects(projectData);
      } catch (err) {
        console.error("Error fetching portfolio data:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return (
      <div style={{ height: '100vh', display: 'flex', justifyContent: 'center', alignItems: 'center', background: 'var(--bg-primary)', color: 'var(--accent-blue)', fontSize: '1.2rem', fontWeight: 600 }}>
        Loading Senior Engineer Portfolio...
      </div>
    );
  }

  return (
    <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
      <Navbar />
      <main style={{ flex: 1 }}>
        <Hero profile={profile} />
        <About profile={profile} />
        <Experience experiences={experiences} />
        <Skills skills={skills} />
        <Projects projects={projects} />
        <ContactForm />
      </main>
      <Footer />
    </div>
  );
}
