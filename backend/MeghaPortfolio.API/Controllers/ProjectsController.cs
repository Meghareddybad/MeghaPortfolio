using Microsoft.AspNetCore.Mvc;
using MeghaPortfolio.API.Core.Application.DTOs;
using MeghaPortfolio.API.Core.Application.Interfaces;

namespace MeghaPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProjectsController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public ProjectsController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    /// <summary>
    /// Fetches all featured projects including SmartStore Flagship platform.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects(CancellationToken cancellationToken)
    {
        var projects = await _portfolioService.GetProjectsAsync(cancellationToken);
        return Ok(projects);
    }

    /// <summary>
    /// Fetches a specific project by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> GetProjectById(int id, CancellationToken cancellationToken)
    {
        var project = await _portfolioService.GetProjectByIdAsync(id, cancellationToken);
        if (project == null)
        {
            return NotFound(new { Message = $"Project with ID {id} not found." });
        }
        return Ok(project);
    }
}
