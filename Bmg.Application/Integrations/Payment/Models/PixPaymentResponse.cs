namespace Bmg.Application.Integrations.Payment.Models;

public record PixPaymentResponse(bool Status, Guid TransactionId) : PaymentResponse(Status, TransactionId);