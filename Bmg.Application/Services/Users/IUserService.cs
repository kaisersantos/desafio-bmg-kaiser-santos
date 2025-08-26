using Bmg.Application.Services.Users.Models;
using Bmg.Domain;

namespace Bmg.Application.Services.Users;

public interface IUserService
{
    Task<CreatedUserResponse> CreateUserAsync(CreateUserRequest createUserRequest, UserRole? userRole = UserRole.Customer);
    Task<VerifyCredentialsResponse?> VerifyCredentialsAsync(AuthRequest authRequest);
}
