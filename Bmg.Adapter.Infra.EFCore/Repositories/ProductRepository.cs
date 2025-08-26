using Bmg.Adapter.Infra.EFCore.Repositories.Base;
using Bmg.Application.Repositories;
using Bmg.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bmg.Adapter.Infra.EFCore.Repositories;

public class ProductRepository(BmgDbContext context) : AuditableRepository<ProductEntity>(context), IProductRepository
{
    public async Task<ProductEntity?> GetByIdAsync(Guid id) =>
        await _context.Products
            .Where(p => p.DeletedAt == null)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<ProductEntity>> GetAllAsync() =>
        await _context.Products
            .Where(p => p.DeletedAt == null)
            .ToListAsync();
}
