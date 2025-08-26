using Bmg.Application.Services.Payments.Models;

namespace Bmg.Application.Services.Payments;

public interface IPaymentService
{
    Task PayAsync(Guid userId, PayRequest payRequest);
}
