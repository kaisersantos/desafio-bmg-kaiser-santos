using Bmg.Application.Services.Products.Models;

namespace Bmg.Application.Services.Products;

public interface IProductService
{
    Task<CreatedProductResponse> CreateAsync(CreateProductRequest createProductRequest);
    Task AddStockAsync(Guid id, AddStockProductRequest addStockProductRequest);
    Task EditAsync(Guid id, EditProductRequest editProductRequest);
    Task DeleteAsync(Guid id);
    Task<GetProductResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<GetProductResponse>> GetAllAsync();
}
