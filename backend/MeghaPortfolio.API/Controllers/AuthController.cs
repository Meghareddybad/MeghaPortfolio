using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MeghaPortfolio.API.Core.Application.DTOs;

namespace MeghaPortfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Authenticates administrator credentials and returns a JWT Bearer Token.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        var expectedUsername = _configuration["AdminSettings:Username"] 
            ?? Environment.GetEnvironmentVariable("ADMIN_USERNAME") 
            ?? "admin";
            
        var expectedPassword = _configuration["AdminSettings:Password"] 
            ?? Environment.GetEnvironmentVariable("ADMIN_PASSWORD") 
            ?? "MeghaPortfolio_Admin2026_SecureKey!#9876";

        if (request.Username != expectedUsername || request.Password != expectedPassword)
        {
            return Unauthorized(new { Message = "Invalid administrator username or password." });
        }

        var secretKey = _configuration["Jwt:SecretKey"] 
            ?? Environment.GetEnvironmentVariable("JWT_SECRET") 
            ?? "SuperSecretKey_MeghaPortfolio_SeniorDotNetDeveloper_2026_SecureKey!";

        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var tokenHandler = new JwtSecurityTokenHandler();
        var expiresAt = DateTime.UtcNow.AddHours(8);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.Username),
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "Admin")
            }),
            Expires = expiresAt,
            Issuer = _configuration["Jwt:Issuer"] ?? "MeghaPortfolioAPI",
            Audience = _configuration["Jwt:Audience"] ?? "MeghaPortfolioAdmin",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new LoginResponseDto
        {
            Token = tokenString,
            Username = request.Username,
            Role = "Admin",
            ExpiresAt = expiresAt
        });
    }
}
