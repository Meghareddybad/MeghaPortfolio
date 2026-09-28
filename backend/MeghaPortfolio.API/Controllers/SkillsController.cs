using Microsoft.AspNetCore.Mvc;
using MeghaPortfolio.API.Core.Application.DTOs;
using MeghaPortfolio.API.Core.Application.Interfaces;

namespace MeghaPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SkillsController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public SkillsController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    /// <summary>
    /// Fetches technical skills categorized by domain.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SkillDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills(CancellationToken cancellationToken)
    {
        var skills = await _portfolioService.GetSkillsAsync(cancellationToken);
        return Ok(skills);
    }
}
