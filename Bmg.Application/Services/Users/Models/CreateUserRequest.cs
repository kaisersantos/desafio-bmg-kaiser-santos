namespace Bmg.Application.Services.Users.Models;

public record CreateUserRequest(
    string Name,
    string Email,
    string Password
)
{
    public string Name { get; init; } = Name?.Trim() ?? string.Empty;
    public string Email { get; init; } = Email?.Trim().ToLowerInvariant() ?? string.Empty;
};