using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Retrieves all submitted contact messages. Accessible ONLY by Administrator.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<ContactMessageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<ContactMessageResponseDto>>> GetMessages(CancellationToken cancellationToken)
    {
        var messages = await _portfolioService.GetContactMessagesAsync(cancellationToken);
        return Ok(messages);
    }

    /// <summary>
    /// Retrieves a single contact message by ID. Accessible ONLY by Administrator.
    /// </summary>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ContactMessageResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ContactMessageResponseDto>> GetMessageById(int id, CancellationToken cancellationToken)
    {
        var message = await _portfolioService.GetContactMessageByIdAsync(id, cancellationToken);
        if (message == null) return NotFound(new { Message = $"Contact message with ID {id} was not found." });
        return Ok(message);
    }

    /// <summary>
    /// Updates the read/unread status of a contact message. Accessible ONLY by Administrator.
    /// </summary>
    [HttpPatch("{id:int}/read")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateReadStatus(int id, [FromBody] bool isRead, CancellationToken cancellationToken)
    {
        var success = await _portfolioService.UpdateContactMessageReadStatusAsync(id, isRead, cancellationToken);
        if (!success) return NotFound(new { Message = $"Contact message with ID {id} was not found." });
        return Ok(new { Message = $"Message {id} read status updated to {isRead}." });
    }

    /// <summary>
    /// Deletes a contact message by ID. Accessible ONLY by Administrator.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteMessage(int id, CancellationToken cancellationToken)
    {
        var success = await _portfolioService.DeleteContactMessageAsync(id, cancellationToken);
        if (!success) return NotFound(new { Message = $"Contact message with ID {id} was not found." });
        return Ok(new { Message = $"Contact message with ID {id} deleted successfully." });
    }
}
