using Bmg.Domain;

namespace Bmg.Application.Repositories;

public interface IPaymentRepository
{
    Task<PaymentEntity> AddAsync(PaymentEntity payment);
    Task<PaymentEntity?> GetByIdAsync(Guid id);
}