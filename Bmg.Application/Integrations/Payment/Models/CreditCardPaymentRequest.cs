namespace Bmg.Application.Integrations.Payment.Models;

public record CreditCardPaymentRequest : PaymentRequest
{
    public string CardNumber { get; set; } = string.Empty;
    public string CardHolder { get; set; } = string.Empty;
    public string Expiration { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
};