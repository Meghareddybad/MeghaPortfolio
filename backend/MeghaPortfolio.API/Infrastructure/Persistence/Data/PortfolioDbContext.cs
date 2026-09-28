using Microsoft.EntityFrameworkCore;
using MeghaPortfolio.API.Core.Domain.Entities;

namespace MeghaPortfolio.API.Infrastructure.Persistence.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
    {
    }

    public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
    public DbSet<ExperienceEntity> Experiences => Set<ExperienceEntity>();
    public DbSet<SkillEntity> Skills => Set<SkillEntity>();
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<ContactMessageEntity> ContactMessages => Set<ContactMessageEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity Configurations
        modelBuilder.Entity<ProfileEntity>().HasKey(p => p.Id);
        modelBuilder.Entity<ExperienceEntity>().HasKey(e => e.Id);
        modelBuilder.Entity<SkillEntity>().HasKey(s => s.Id);
        modelBuilder.Entity<ProjectEntity>().HasKey(pr => pr.Id);
        modelBuilder.Entity<ContactMessageEntity>().HasKey(c => c.Id);
    }
}
