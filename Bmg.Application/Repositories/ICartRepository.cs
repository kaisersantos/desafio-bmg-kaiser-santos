using Bmg.Domain;

namespace Bmg.Application.Repositories;

public interface ICartRepository
{
    Task<CartEntity> AddAsync(CartEntity cart);
    Task SoftDeleteAsync(CartEntity cart);
    Task<CartEntity?> GetCurrentAsync(Guid idUser);
    Task<IEnumerable<CartEntity>> GetHistoryAsync(Guid idUser);
}