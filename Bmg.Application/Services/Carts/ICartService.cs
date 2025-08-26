using Bmg.Application.Services.Carts.Models;

namespace Bmg.Application.Services.Carts;

public interface ICartService
{
    Task AddItemAsync(Guid userId, AddCartItemRequest addCartItemRequest);
    Task RemoveItemAsync(Guid userId, Guid productId, RemoveCartItemRequest removeCartItemRequest);
    Task<GetCurrentResponse> GetCurrentCartAsync(Guid userId);
    Task<IEnumerable<GetHistoryResponse>> GetHistoryAsync(Guid userId);
}
