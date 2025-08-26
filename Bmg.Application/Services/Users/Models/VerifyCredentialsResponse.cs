using Bmg.Domain;

namespace Bmg.Application.Services.Users.Models;

public record VerifyCredentialsResponse(
    Guid Id,
    string Email,
    UserRole Role
);