using Bmg.Domain;

namespace Bmg.Application.Services.Payments.Models;

public class PayByCreditCardRequest : PayRequest
{
    public string CardNumber { get; set; } = string.Empty;
    public string CardHolder { get; set; } = string.Empty;
    public string Expiration { get; set; } = string.Empty;
    public short Cvv { get; set; }
    public override PaymentMethod Method => PaymentMethod.CreditCard;
}