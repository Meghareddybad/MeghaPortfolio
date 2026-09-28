using Microsoft.AspNetCore.Mvc;
using MeghaPortfolio.API.Core.Application.DTOs;
using MeghaPortfolio.API.Core.Application.Interfaces;

namespace MeghaPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ExperienceController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public ExperienceController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    /// <summary>
    /// Fetches professional experience history and achievements.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExperienceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExperienceDto>>> GetExperiences(CancellationToken cancellationToken)
    {
        var experiences = await _portfolioService.GetExperiencesAsync(cancellationToken);
        return Ok(experiences);
    }
}
