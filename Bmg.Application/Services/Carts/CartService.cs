using AutoMapper;
using Bmg.Application.Exceptions;
using Bmg.Application.Repositories;
using Bmg.Application.Services.Carts.Models;
using Bmg.Domain;
using FluentValidation;

namespace Bmg.Application.Services.Carts;

public class CartService(
    IMapper mapper,
    IValidator<AddCartItemRequest> addCartItemRequestValidator,
    IValidator<RemoveCartItemRequest> removeCartItemRequestValidator,
    ICartRepository cartRepository,
    ICartItemRepository cartItemRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : ICartService
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IValidator<AddCartItemRequest> _addCartItemRequestValidator = addCartItemRequestValidator ?? throw new ArgumentNullException(nameof(addCartItemRequestValidator));
    private readonly IValidator<RemoveCartItemRequest> _removeCartItemRequestValidator = removeCartItemRequestValidator ?? throw new ArgumentNullException(nameof(removeCartItemRequestValidator));
    private readonly ICartRepository _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    private readonly ICartItemRepository _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task AddItemAsync(Guid userId, AddCartItemRequest addCartItemRequest)
    {
        await _addCartItemRequestValidator.ValidateAndThrowAsync(addCartItemRequest);

        await _unitOfWork.ExecuteAsync(async () =>
        {
            var currentCart = await GetCurrentCartOrCreateAsync(userId);

            var productEntity = await _productRepository.GetByIdAsync(addCartItemRequest.ProductId) ??
                throw new NotFoundException($"O produto com ID {addCartItemRequest.ProductId} não foi encontrado.");

            if (productEntity.Stock < addCartItemRequest.Quantity)
                throw new BusinessErrorException($"O produto com ID {addCartItemRequest.ProductId} não possui estoque suficiente.");

            productEntity.Stock -= addCartItemRequest.Quantity;
            await _productRepository.UpdateAsync(productEntity);

            await AddOrUpdateCartItemAsync(currentCart, addCartItemRequest);
        });
    }

    public async Task RemoveItemAsync(Guid userId, Guid productId, RemoveCartItemRequest removeCartItemRequest)
    {
        await _removeCartItemRequestValidator.ValidateAndThrowAsync(removeCartItemRequest);

        await _unitOfWork.ExecuteAsync(async () =>
        {
            var cartEntity = await _cartRepository.GetCurrentAsync(userId) ??
                throw new NotFoundException($"O carrinho deste usuário não foi encontrado.");

            var productEntity = await _productRepository.GetByIdAsync(productId) ??
                throw new NotFoundException($"O produto com ID {productId} não foi encontrado.");

            var cartItemEntity = await _cartItemRepository.GetByCartIdAndProductIdAsync(cartEntity.Id, productId) ??
                throw new NotFoundException($"O item do carrinho com ID {productId} não foi encontrado.");

            if (removeCartItemRequest.Quantity > cartItemEntity.Quantity)
                throw new BusinessErrorException($"A quantidade a ser removida é maior que a quantidade no carrinho.");

            if (removeCartItemRequest.Quantity == cartItemEntity.Quantity)
                await _cartItemRepository.HardDeleteAsync(cartItemEntity);
            else
                cartItemEntity.Quantity -= removeCartItemRequest.Quantity;

            productEntity.Stock += removeCartItemRequest.Quantity;
            await _productRepository.UpdateAsync(productEntity);

            await DeleteCurrentCartIfNoItemsAsync(userId);
        });
    }

    public async Task<GetCurrentResponse> GetCurrentCartAsync(Guid userId)
    {
        var cartEntity = await GetCurrentCartOrCreateAsync(userId);

        var response = _mapper.Map<GetCurrentResponse>(cartEntity);
        return response;
    }

    public async Task<IEnumerable<GetHistoryResponse>> GetHistoryAsync(Guid userId)
    {
        var cartEntities = await _cartRepository.GetHistoryAsync(userId);

        var response = _mapper.Map<IEnumerable<GetHistoryResponse>>(cartEntities);
        return response;
    }

    private async Task<CartEntity> GetCurrentCartOrCreateAsync(Guid userId)
    {
        var cart = await _cartRepository.GetCurrentAsync(userId);
        cart ??= await _cartRepository.AddAsync(new CartEntity
        {
            UserId = userId
        });
        return cart;
    }

    private async Task AddOrUpdateCartItemAsync(CartEntity currentCart, AddCartItemRequest addCartItemRequest)
    {
        var existingItem = currentCart.Items.FirstOrDefault(i => i.ProductId == addCartItemRequest.ProductId);
        if (existingItem is not null)
        {
            existingItem.Quantity += addCartItemRequest.Quantity;
            return;
        }

        var cartItemEntity = _mapper.Map<CartItemEntity>(addCartItemRequest);
        cartItemEntity.CartId = currentCart.Id;
        await _cartItemRepository.AddAsync(cartItemEntity);
    }

    private async Task DeleteCurrentCartIfNoItemsAsync(Guid userId)
    {
        var cartEntity = await _cartRepository.GetCurrentAsync(userId);
        if (cartEntity is not null && cartEntity.Items.Count == 0)
            await _cartRepository.SoftDeleteAsync(cartEntity);
    }
}
