namespace Bmg.Application.Services.Jwt.Models;

public record CreateJwtRequest
{
    public Guid UserId { get; set; } = Guid.Empty;
    public string UserRole { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiryHours { get; set; }
};