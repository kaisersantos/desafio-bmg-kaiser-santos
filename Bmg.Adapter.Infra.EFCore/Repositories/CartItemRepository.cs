using Bmg.Adapter.Infra.EFCore.Repositories.Base;
using Bmg.Application.Repositories;
using Bmg.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bmg.Adapter.Infra.EFCore.Repositories;

public class CartItemRepository(BmgDbContext context) : NonAuditableRepository<CartItemEntity>(context), ICartItemRepository
{
    public async Task<bool> UsedProductAsync(Guid productId) =>
        await _context.CartItems
            .AnyAsync(p => p.ProductId == productId);

    public async Task<CartItemEntity?> GetByCartIdAndProductIdAsync(Guid cartId, Guid productId) =>
        await _context.CartItems
            .FirstOrDefaultAsync(p => p.CartId == cartId && p.ProductId == productId);
}
