namespace Bmg.Application.Services.Users.Models;

public record AuthRequest(
    string Email,
    string Password
);