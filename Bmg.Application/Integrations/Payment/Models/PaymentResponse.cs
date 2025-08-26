namespace Bmg.Application.Integrations.Payment.Models;

public record PaymentResponse(bool Status, Guid TransactionId);