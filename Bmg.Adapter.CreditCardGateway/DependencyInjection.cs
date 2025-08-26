using Bmg.Application.Integrations.Payment;
using Bmg.Application.Integrations.Payment.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Bmg.Adapter.CreditCardGateway;

public static class DependencyInjection
{
    public static IServiceCollection AddCreditCardGateway(this IServiceCollection services)
    {
        services.AddScoped<IPaymentGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>, FakeCreditCardGateway>();

        return services;
    }
}
