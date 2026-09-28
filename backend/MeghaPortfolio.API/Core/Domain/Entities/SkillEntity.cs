namespace MeghaPortfolio.API.Core.Domain.Entities;

public class SkillEntity
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty; // Backend, Databases, Testing, Architecture, Cloud, DevOps, Frontend
    public string SkillList { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}
