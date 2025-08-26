using Bmg.Domain;

namespace Bmg.Application.Services.Users.Models;

public record CreatedUserResponse(
    string Name,
    string Email,
    UserRole Role,
    DateTime CreatedAt,
    DateTime UpdatedAt
);