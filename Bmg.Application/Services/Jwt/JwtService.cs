using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bmg.Application.Services.Jwt.Models;
using Microsoft.IdentityModel.Tokens;

namespace Bmg.Application.Services.Jwt;

public class JwtService : IJwtService
{
    public string GenerateJwtToken(CreateJwtRequest createJwtRequest)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(createJwtRequest.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, createJwtRequest.UserId.ToString()),
                new Claim(ClaimTypes.Email, createJwtRequest.UserEmail),
                new Claim(ClaimTypes.Role, createJwtRequest.UserRole)
            ]),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = createJwtRequest.Issuer,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddHours(createJwtRequest.ExpiryHours),
            Audience = createJwtRequest.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
