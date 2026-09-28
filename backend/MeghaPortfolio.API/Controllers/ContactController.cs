using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MeghaPortfolio.API.Core.Application.DTOs;
using MeghaPortfolio.API.Core.Application.Interfaces;

namespace MeghaPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ContactController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public ContactController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    /// <summary>
    /// Submits a contact message from recruiters or hiring managers. Protected by Rate Limiting (5 requests/min).
    /// </summary>
    [HttpPost]
    [EnableRateLimiting("ContactFormLimiter")]
    [ProducesResponseType(typeof(ContactMessageResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public async Task<ActionResult<ContactMessageResponseDto>> SubmitMessage(
        [FromBody] ContactMessageCreateDto request, 
        CancellationToken cancellationToken)
    {
        // Model validation
        if (string.IsNullOrWhiteSpace(request.SenderName))
        {
            return BadRequest(new { Message = "Sender name is required." });
        }

        if (string.IsNullOrWhiteSpace(request.SenderEmail) || !request.SenderEmail.Contains("@"))
        {
            return BadRequest(new { Message = "A valid email address is required." });
        }

        if (string.IsNullOrWhiteSpace(request.Message) || request.Message.Length < 10)
        {
            return BadRequest(new { Message = "Message content must be at least 10 characters long." });
        }

        var result = await _portfolioService.SubmitContactMessageAsync(request, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
