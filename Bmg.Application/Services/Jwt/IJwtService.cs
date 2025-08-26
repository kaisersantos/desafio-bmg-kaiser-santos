using Bmg.Application.Services.Jwt.Models;

namespace Bmg.Application.Services.Jwt;

public interface IJwtService
{
    string GenerateJwtToken(CreateJwtRequest createJwtRequest);
}
