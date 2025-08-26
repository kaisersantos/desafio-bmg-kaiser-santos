namespace Bmg.Application.Integrations.Payment.Models;

public record PaymentRequest
{
    public decimal Amount { get; set; }
};