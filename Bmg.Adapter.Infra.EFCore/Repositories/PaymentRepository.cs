using Bmg.Adapter.Infra.EFCore.Repositories.Base;
using Bmg.Application.Repositories;
using Bmg.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bmg.Adapter.Infra.EFCore.Repositories;

public class PaymentRepository(BmgDbContext context) : AuditableRepository<PaymentEntity>(context), IPaymentRepository
{
    public async Task<PaymentEntity?> GetByIdAsync(Guid id) =>
        await _context.Payments
            .Where(p => p.DeletedAt == null)
            .FirstOrDefaultAsync(p => p.Id == id);
}
