using AutoMapper;
using Bmg.Application.Exceptions;
using Bmg.Application.Repositories;
using Bmg.Application.Services.Products.Models;
using Bmg.Domain;
using FluentValidation;

namespace Bmg.Application.Services.Products;

public class ProductService(
    IMapper mapper,
    IValidator<CreateProductRequest> createValidator,
    IValidator<AddStockProductRequest> addStockValidator,
    IValidator<EditProductRequest> editValidator,
    IProductRepository productRepository,
    ICartItemRepository cartItemRepository) : IProductService
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IValidator<CreateProductRequest> _createValidator = createValidator ?? throw new ArgumentNullException(nameof(createValidator));
    private readonly IValidator<AddStockProductRequest> _addStockValidator = addStockValidator ?? throw new ArgumentNullException(nameof(addStockValidator));
    private readonly IValidator<EditProductRequest> _editValidator = editValidator ?? throw new ArgumentNullException(nameof(editValidator));
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly ICartItemRepository _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));

    public async Task<CreatedProductResponse> CreateAsync(CreateProductRequest createProductRequest)
    {
        await _createValidator.ValidateAndThrowAsync(createProductRequest);

        var productEntity = _mapper.Map<ProductEntity>(createProductRequest);
        await _productRepository.AddAsync(productEntity);

        var response = _mapper.Map<CreatedProductResponse>(productEntity);
        return response;
    }

    public async Task AddStockAsync(Guid id, AddStockProductRequest addStockProductRequest)
    {
        await _addStockValidator.ValidateAndThrowAsync(addStockProductRequest);

        var productEntity = await _productRepository.GetByIdAsync(id) ??
            throw new NotFoundException($"O produto com ID {id} não foi encontrado.");

        productEntity.Stock += addStockProductRequest.Quantity;
        await _productRepository.UpdateAsync(productEntity);
    }

    public async Task EditAsync(Guid id, EditProductRequest editProductRequest)
    {
        await _editValidator.ValidateAndThrowAsync(editProductRequest);

        var productEntity = await _productRepository.GetByIdAsync(id) ??
            throw new NotFoundException($"O produto com ID {id} não foi encontrado.");

        _mapper.Map(editProductRequest, productEntity);
        await _productRepository.UpdateAsync(productEntity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var productEntity = await _productRepository.GetByIdAsync(id) ??
            throw new NotFoundException($"O produto com ID {id} não foi encontrado.");

        if (await _cartItemRepository.UsedProductAsync(id))
            throw new BusinessErrorException($"O produto com ID {id} não pode ser excluído porque já foi utilizado em um carrinho.");

        await _productRepository.SoftDeleteAsync(productEntity);
    }

    public async Task<GetProductResponse> GetByIdAsync(Guid id)
    {
        var productEntity = await _productRepository.GetByIdAsync(id) ??
            throw new NotFoundException($"O produto com ID {id} não foi encontrado.");

        var response = _mapper.Map<GetProductResponse>(productEntity);
        return response;
    }

    public async Task<IEnumerable<GetProductResponse>> GetAllAsync()
    {
        var productEntities = await _productRepository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<GetProductResponse>>(productEntities);
        return response;
    }
}
