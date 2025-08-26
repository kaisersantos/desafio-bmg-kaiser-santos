using Microsoft.AspNetCore.Mvc;
using Bmg.Application.Services.Payments;
using Bmg.Application.Services.Payments.Models;
using Bmg.Api.Attributes;
using Bmg.Domain;
using Bmg.Api.Utils;

namespace Bmg.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[AuthorizeRole(UserRole.Customer)]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));

    [HttpPost("credit-card")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PayByCreditCard(PayByCreditCardRequest payByCreditCardRequest)
    {
        var userId = HttpContext.GetUserId();

        await _paymentService.PayAsync(userId, payByCreditCardRequest);
        return NoContent();
    }

    [HttpPost("pix")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PayByPix(PayByPixRequest payByPixRequest)
    {
        var userId = HttpContext.GetUserId();

        await _paymentService.PayAsync(userId, payByPixRequest);
        return NoContent();
    }
}

