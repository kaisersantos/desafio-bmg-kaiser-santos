using System.Security.Claims;

namespace Bmg.Api.Utils;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        if (context.User?.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
            throw new UnauthorizedAccessException("Usuário não autenticado");

        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        return userId;
    }
}
