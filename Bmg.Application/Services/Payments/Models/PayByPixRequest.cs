using Bmg.Domain;

namespace Bmg.Application.Services.Payments.Models;

public class PayByPixRequest : PayRequest
{
    public string PixKey { get; set; } = string.Empty;
    public string? Description { get; set; }
    public override PaymentMethod Method => PaymentMethod.Pix;
}
