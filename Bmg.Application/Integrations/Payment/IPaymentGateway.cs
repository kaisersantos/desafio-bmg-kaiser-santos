namespace Bmg.Application.Integrations.Payment;

public interface IPaymentGateway<TPaymentRequest, TPaymentResponse>
{
    Task<TPaymentResponse> ProcessPaymentAsync(TPaymentRequest payment);
}