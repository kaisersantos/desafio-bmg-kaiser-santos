using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bmg.Application.Services.Users;
using Bmg.Application.Services.Jwt;
using Bmg.Application.Services.Users.Models;
using Bmg.Api.Settings;
using AutoMapper;
using Bmg.Application.Services.Jwt.Models;

namespace Bmg.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[AllowAnonymous]
public class AuthController(
    IMapper mapper,
    IUserService userService,
    IJwtService jwtService,
    JwtSettings jwtSettings) : ControllerBase
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    private readonly IJwtService _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    private readonly JwtSettings _jwtSettings = jwtSettings;

    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenResponse>> SignIn(AuthRequest authRequest)
    {
        var user = await _userService.VerifyCredentialsAsync(authRequest);

        var jwtRequest = _mapper.Map<CreateJwtRequest>(_jwtSettings);
        _mapper.Map(user, jwtRequest);

        var token = _jwtService.GenerateJwtToken(jwtRequest);
        return Ok(new TokenResponse(token));
    }
}
