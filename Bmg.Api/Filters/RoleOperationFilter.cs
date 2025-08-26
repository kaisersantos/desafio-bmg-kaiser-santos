using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Bmg.Api.Attributes;
using System.Reflection;

namespace Bmg.Api.Filters;

public class RoleOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionRoles = context.MethodInfo.GetCustomAttributes<AuthorizeRoleAttribute>();
        var controllerRoles = context.MethodInfo.DeclaringType?.GetCustomAttributes<AuthorizeRoleAttribute>() ?? [];

        var roles = actionRoles.Concat(controllerRoles).SelectMany(a => a.Roles).Distinct().ToArray();

        if (roles.Length != 0)
            operation.Description += $"<br/><b>Roles autorizadas:</b> {string.Join(", ", roles)}";
    }
}
