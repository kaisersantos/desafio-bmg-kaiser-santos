using Bmg.Application.Integrations.Payment.Models;
using Bmg.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Bmg.Application.Integrations.Payment;

public class PaymentGatewayFactory(IServiceProvider serviceProvider) : IPaymentGatewayFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IPaymentGateway<TPaymentRequest, TPaymentResponse> GetGateway<TPaymentRequest, TPaymentResponse>(PaymentMethod method)
        where TPaymentRequest : PaymentRequest
        where TPaymentResponse : PaymentResponse
    {
        return method switch
        {
            PaymentMethod.CreditCard => (IPaymentGateway<TPaymentRequest, TPaymentResponse>)_serviceProvider.GetRequiredService<IPaymentGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>>(),
            PaymentMethod.Pix => (IPaymentGateway<TPaymentRequest, TPaymentResponse>)_serviceProvider.GetRequiredService<IPaymentGateway<PixPaymentRequest, PixPaymentResponse>>(),
            _ => throw new ArgumentException("Invalid Payment Method", nameof(method))
        };
    }
}