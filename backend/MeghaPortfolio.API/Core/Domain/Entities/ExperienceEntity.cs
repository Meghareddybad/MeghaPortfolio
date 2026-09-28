namespace MeghaPortfolio.API.Core.Domain.Entities;

public class ExperienceEntity
{
    public int Id { get; set; }
    public string Company { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string DateRange { get; set; } = string.Empty;
    public bool IsCurrentRole { get; set; }
    public List<string> Responsibilities { get; set; } = new();
    public List<string> Technologies { get; set; } = new();
    public string KeyHighlight { get; set; } = string.Empty;
}
