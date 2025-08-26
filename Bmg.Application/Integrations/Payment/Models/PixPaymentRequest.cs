namespace Bmg.Application.Integrations.Payment.Models;

public record PixPaymentRequest : PaymentRequest
{
    public string PixKey { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string? Description { get; set; }
};