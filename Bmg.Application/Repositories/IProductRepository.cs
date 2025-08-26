using Bmg.Domain;

namespace Bmg.Application.Repositories;

public interface IProductRepository
{
    Task<ProductEntity> AddAsync(ProductEntity product);
    Task UpdateAsync(ProductEntity product);
    Task SoftDeleteAsync(ProductEntity product);
    Task<ProductEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductEntity>> GetAllAsync();
}