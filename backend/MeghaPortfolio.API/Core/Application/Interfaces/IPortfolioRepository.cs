using MeghaPortfolio.API.Core.Domain.Entities;

namespace MeghaPortfolio.API.Core.Application.Interfaces;

public interface IPortfolioRepository
{
    Task<ProfileEntity?> GetProfileAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ExperienceEntity>> GetExperiencesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<SkillEntity>> GetSkillsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectEntity>> GetProjectsAsync(CancellationToken cancellationToken = default);
    Task<ProjectEntity?> GetProjectByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ContactMessageEntity> AddContactMessageAsync(ContactMessageEntity entity, CancellationToken cancellationToken = default);
}
