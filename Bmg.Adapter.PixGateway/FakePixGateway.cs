using Bmg.Application.Integrations.Payment;
using Bmg.Application.Integrations.Payment.Models;

namespace Bmg.Adapter.PixGateway;

public class FakePixGateway : IPaymentGateway<PixPaymentRequest, PixPaymentResponse>
{
    public async Task<PixPaymentResponse> ProcessPaymentAsync(PixPaymentRequest payment)
    {
        var random = new Random();

        await Task.Delay(random.Next(0, 1001));

        return new PixPaymentResponse(random.Next(0, 2) == 1, Guid.NewGuid());
    }
}