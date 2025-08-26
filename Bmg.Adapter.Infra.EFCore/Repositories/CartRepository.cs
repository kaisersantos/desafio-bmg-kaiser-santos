using Bmg.Domain;
using Bmg.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Bmg.Adapter.Infra.EFCore.Repositories.Base;

namespace Bmg.Adapter.Infra.EFCore.Repositories;

public class CartRepository(BmgDbContext context) : AuditableRepository<CartEntity>(context), ICartRepository
{
    public async Task<CartEntity?> GetCurrentAsync(Guid idUser) =>
        await _context.Carts
            .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
            .Where(c => c.UserId == idUser)
            .Where(c => c.DeletedAt == null)
            .FirstOrDefaultAsync(c => c.Payment == null);

    public async Task<IEnumerable<CartEntity>> GetHistoryAsync(Guid idUser) =>
        await _context.Carts
            .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
            .Include(c => c.Payment)
            .Where(c => c.Payment != null)
            .Where(c => c.UserId == idUser)
            .Where(c => c.DeletedAt == null)
            .ToListAsync();
}
