using Bmg.Domain;

namespace Bmg.Application.Repositories;

public interface ICartItemRepository
{
    Task<CartItemEntity> AddAsync(CartItemEntity cartItem);
    Task UpdateAsync(CartItemEntity cartItem);
    Task HardDeleteAsync(CartItemEntity cartItem);
    Task<bool> UsedProductAsync(Guid productId);
    Task<CartItemEntity?> GetByCartIdAndProductIdAsync(Guid cartId, Guid productId);
}