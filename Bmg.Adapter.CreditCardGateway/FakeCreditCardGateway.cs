using Bmg.Application.Integrations.Payment;
using Bmg.Application.Integrations.Payment.Models;

namespace Bmg.Adapter.CreditCardGateway;

public class FakeCreditCardGateway : IPaymentGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>
{
    public async Task<CreditCardPaymentResponse> ProcessPaymentAsync(CreditCardPaymentRequest payment)
    {
        var random = new Random();

        await Task.Delay(random.Next(0, 1001));

        return new CreditCardPaymentResponse(random.Next(0, 2) == 1, Guid.NewGuid());
    }
}