namespace MeghaPortfolio.API.Core.Application.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string Architecture { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new();
    public string KeyContribution { get; set; } = string.Empty;
    public string MeasurableResult { get; set; } = string.Empty;
    public bool IsFlagship { get; set; }
    public string? GitHubUrl { get; set; }
    public string? LiveDemoUrl { get; set; }
}
