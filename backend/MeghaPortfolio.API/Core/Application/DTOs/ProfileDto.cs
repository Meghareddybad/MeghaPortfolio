namespace MeghaPortfolio.API.Core.Application.DTOs;

public class ProfileDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public string GitHubUrl { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string LatencyReductionMetric { get; set; } = string.Empty;
    public string TestCoverageMetric { get; set; } = string.Empty;
}
