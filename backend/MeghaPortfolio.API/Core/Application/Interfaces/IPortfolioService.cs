using MeghaPortfolio.API.Core.Application.DTOs;

namespace MeghaPortfolio.API.Core.Application.Interfaces;

public interface IPortfolioService
{
    Task<ProfileDto?> GetProfileAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ExperienceDto>> GetExperiencesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<SkillDto>> GetSkillsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectDto>> GetProjectsAsync(CancellationToken cancellationToken = default);
    Task<ProjectDto?> GetProjectByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ContactMessageResponseDto> SubmitContactMessageAsync(ContactMessageCreateDto dto, CancellationToken cancellationToken = default);
    Task<IEnumerable<ContactMessageResponseDto>> GetContactMessagesAsync(CancellationToken cancellationToken = default);
    Task<ContactMessageResponseDto?> GetContactMessageByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> UpdateContactMessageReadStatusAsync(int id, bool isRead, CancellationToken cancellationToken = default);
    Task<bool> DeleteContactMessageAsync(int id, CancellationToken cancellationToken = default);
}
