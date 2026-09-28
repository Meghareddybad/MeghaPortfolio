using Microsoft.EntityFrameworkCore;
using MeghaPortfolio.API.Core.Application.Interfaces;
using MeghaPortfolio.API.Core.Domain.Entities;
using MeghaPortfolio.API.Infrastructure.Persistence.Data;

namespace MeghaPortfolio.API.Infrastructure.Persistence.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly PortfolioDbContext _context;

    public PortfolioRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<ProfileEntity?> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Profiles
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<ExperienceEntity>> GetExperiencesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Experiences
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SkillEntity>> GetSkillsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Skills
            .AsNoTracking()
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProjectEntity>> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .AsNoTracking()
            .OrderByDescending(p => p.IsFlagship)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProjectEntity?> GetProjectByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<ContactMessageEntity> AddContactMessageAsync(ContactMessageEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.ContactMessages.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
