namespace Bmg.Application.Integrations.Payment.Models;

public record CreditCardPaymentResponse(bool Status, Guid TransactionId) : PaymentResponse(Status, TransactionId);