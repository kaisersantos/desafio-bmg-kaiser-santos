using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bmg.Application.Services.Users;
using Bmg.Application.Services.Users.Models;
using Bmg.Domain;
using Bmg.Api.Attributes;

namespace Bmg.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CreatedUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreatedUserResponse>> Post(CreateUserRequest createUserRequest)
    {
        var user = await _userService.CreateUserAsync(createUserRequest);
        return CreatedAtAction(nameof(Post), user);
    }

    [HttpPost("admin")]
    [AuthorizeRole(UserRole.Admin)]
    [ProducesResponseType(typeof(CreatedUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CreatedUserResponse>> CreateAdmin(CreateUserRequest createUserRequest)
    {
        var user = await _userService.CreateUserAsync(createUserRequest, UserRole.Admin);
        return CreatedAtAction(nameof(CreateAdmin), user);
    }
}

