namespace MeghaPortfolio.API.Core.Application.DTOs;

public class SkillDto
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string SkillList { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}
