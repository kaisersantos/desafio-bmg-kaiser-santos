using System.Security.Claims;
using Bmg.Application.Exceptions;
using Bmg.Application.Utils;
using Bmg.Domain;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bmg.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeRoleAttribute(params UserRole[] roles) : Attribute, IAsyncActionFilter
{
    private readonly string[] _roles = [.. roles.Select(r => r.GetDescription())];
    
    public string[] Roles => _roles;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (user?.Identity is null || !user.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var roleClaim = user.FindFirst(ClaimTypes.Role)?.Value;
        if (roleClaim is null || !_roles.Contains(roleClaim))
            throw new ForbiddenException("Usuário não autorizado.");

        await next();
    }
}
