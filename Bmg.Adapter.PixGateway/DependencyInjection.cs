using Bmg.Application.Integrations.Payment;
using Bmg.Application.Integrations.Payment.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Bmg.Adapter.PixGateway;

public static class DependencyInjection
{
    public static IServiceCollection AddPixGateway(this IServiceCollection services)
    {
        services.AddScoped<IPaymentGateway<PixPaymentRequest, PixPaymentResponse>, FakePixGateway>();

        return services;
    }
}
