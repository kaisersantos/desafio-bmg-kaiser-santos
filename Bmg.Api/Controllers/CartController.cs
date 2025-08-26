using Microsoft.AspNetCore.Mvc;
using Bmg.Application.Services.Carts;
using Bmg.Application.Services.Carts.Models;
using Bmg.Api.Attributes;
using Bmg.Domain;
using Bmg.Api.Utils;

namespace Bmg.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[AuthorizeRole(UserRole.Customer)]
public class CartController(ICartService cartService) : ControllerBase
{
    private readonly ICartService _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));

    [HttpPost("add-item")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddItem(AddCartItemRequest addCartItemRequest)
    {
        var userId = HttpContext.GetUserId();

        await _cartService.AddItemAsync(userId, addCartItemRequest);
        return NoContent();
    }

    [HttpPatch("{productId:guid}/remove-item")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> RemoveItem(Guid productId, RemoveCartItemRequest removeCartItemRequest)
    {
        var userId = HttpContext.GetUserId();

        await _cartService.RemoveItemAsync(userId, productId, removeCartItemRequest);
        return NoContent();
    }

    [HttpGet("current")]
    [ProducesResponseType(typeof(GetCurrentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<GetCurrentResponse>> GetCurrent()
    {
        var userId = HttpContext.GetUserId();

        var current = await _cartService.GetCurrentCartAsync(userId);
        return Ok(current);
    }

    [HttpGet("history")]
    [ProducesResponseType(typeof(List<GetHistoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<GetHistoryResponse>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<GetHistoryResponse>>> GetAll()
    {
        var userId = HttpContext.GetUserId();

        var history = await _cartService.GetHistoryAsync(userId);
        if (!history.Any())
            return NoContent();

        return Ok(history);
    }
}

