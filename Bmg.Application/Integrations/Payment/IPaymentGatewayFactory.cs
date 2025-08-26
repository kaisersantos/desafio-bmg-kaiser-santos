using Bmg.Application.Integrations.Payment.Models;
using Bmg.Domain;

namespace Bmg.Application.Integrations.Payment;

public interface IPaymentGatewayFactory
{
    IPaymentGateway<TPaymentRequest, TPaymentResponse> GetGateway<TPaymentRequest, TPaymentResponse>(PaymentMethod method)
        where TPaymentRequest : PaymentRequest
        where TPaymentResponse : PaymentResponse;
}