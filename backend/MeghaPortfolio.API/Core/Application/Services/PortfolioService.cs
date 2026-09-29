using MeghaPortfolio.API.Core.Application.DTOs;
using MeghaPortfolio.API.Core.Application.Interfaces;
using MeghaPortfolio.API.Core.Domain.Entities;

namespace MeghaPortfolio.API.Core.Application.Services;

public class PortfolioService : IPortfolioService
{
    private readonly IPortfolioRepository _repository;

    public PortfolioService(IPortfolioRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProfileDto?> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetProfileAsync(cancellationToken);
        if (entity == null) return null;

        return new ProfileDto
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Title = entity.Title,
            Summary = entity.Summary,
            Location = entity.Location,
            Email = entity.Email,
            Phone = entity.Phone,
            LinkedInUrl = entity.LinkedInUrl,
            GitHubUrl = entity.GitHubUrl,
            YearsOfExperience = entity.YearsOfExperience,
            LatencyReductionMetric = entity.LatencyReductionMetric,
            TestCoverageMetric = entity.TestCoverageMetric
        };
    }

    public async Task<IEnumerable<ExperienceDto>> GetExperiencesAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetExperiencesAsync(cancellationToken);
        return entities.Select(e => new ExperienceDto
        {
            Id = e.Id,
            Company = e.Company,
            Role = e.Role,
            DateRange = e.DateRange,
            IsCurrentRole = e.IsCurrentRole,
            Responsibilities = e.Responsibilities,
            Technologies = e.Technologies,
            KeyHighlight = e.KeyHighlight
        });
    }

    public async Task<IEnumerable<SkillDto>> GetSkillsAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetSkillsAsync(cancellationToken);
        return entities.Select(s => new SkillDto
        {
            Id = s.Id,
            Category = s.Category,
            SkillList = s.SkillList,
            DisplayOrder = s.DisplayOrder
        });
    }

    public async Task<IEnumerable<ProjectDto>> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetProjectsAsync(cancellationToken);
        return entities.Select(p => MapProjectToDto(p));
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetProjectByIdAsync(id, cancellationToken);
        return entity == null ? null : MapProjectToDto(entity);
    }

    public async Task<ContactMessageResponseDto> SubmitContactMessageAsync(ContactMessageCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new ContactMessageEntity
        {
            SenderName = dto.SenderName,
            SenderEmail = dto.SenderEmail,
            Subject = dto.Subject,
            Message = dto.Message,
            CreatedAt = DateTime.UtcNow,
            IsRead = false
        };

        var savedEntity = await _repository.AddContactMessageAsync(entity, cancellationToken);

        return new ContactMessageResponseDto
        {
            Id = savedEntity.Id,
            SenderName = savedEntity.SenderName,
            SenderEmail = savedEntity.SenderEmail,
            Subject = savedEntity.Subject,
            Message = savedEntity.Message,
            CreatedAt = savedEntity.CreatedAt,
            IsRead = savedEntity.IsRead,
            Status = "Message Received Successfully"
        };
    }

    public async Task<IEnumerable<ContactMessageResponseDto>> GetContactMessagesAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetContactMessagesAsync(cancellationToken);
        return entities.Select(e => new ContactMessageResponseDto
        {
            Id = e.Id,
            SenderName = e.SenderName,
            SenderEmail = e.SenderEmail,
            Subject = e.Subject,
            Message = e.Message,
            CreatedAt = e.CreatedAt,
            IsRead = e.IsRead,
            Status = e.IsRead ? "Read" : "Unread"
        });
    }

    public async Task<ContactMessageResponseDto?> GetContactMessageByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetContactMessageByIdAsync(id, cancellationToken);
        if (entity == null) return null;

        return new ContactMessageResponseDto
        {
            Id = entity.Id,
            SenderName = entity.SenderName,
            SenderEmail = entity.SenderEmail,
            Subject = entity.Subject,
            Message = entity.Message,
            CreatedAt = entity.CreatedAt,
            IsRead = entity.IsRead,
            Status = entity.IsRead ? "Read" : "Unread"
        };
    }

    public async Task<bool> UpdateContactMessageReadStatusAsync(int id, bool isRead, CancellationToken cancellationToken = default)
    {
        return await _repository.UpdateContactMessageReadStatusAsync(id, isRead, cancellationToken);
    }

    public async Task<bool> DeleteContactMessageAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteContactMessageAsync(id, cancellationToken);
    }

    private static ProjectDto MapProjectToDto(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Tagline = entity.Tagline,
        Architecture = entity.Architecture,
        Description = entity.Description,
        Technologies = entity.Technologies,
        KeyContribution = entity.KeyContribution,
        MeasurableResult = entity.MeasurableResult,
        IsFlagship = entity.IsFlagship,
        GitHubUrl = entity.GitHubUrl,
        LiveDemoUrl = entity.LiveDemoUrl
    };
}
