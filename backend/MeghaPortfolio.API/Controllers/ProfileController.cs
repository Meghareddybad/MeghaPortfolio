using Microsoft.AspNetCore.Mvc;
using MeghaPortfolio.API.Core.Application.DTOs;
using MeghaPortfolio.API.Core.Application.Interfaces;

namespace MeghaPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProfileController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public ProfileController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    /// <summary>
    /// Fetches senior engineer profile details, title, and key metrics.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProfileDto>> GetProfile(CancellationToken cancellationToken)
    {
        var profile = await _portfolioService.GetProfileAsync(cancellationToken);
        if (profile == null)
        {
            return NotFound(new { Message = "Profile information not found." });
        }
        return Ok(profile);
    }
}
