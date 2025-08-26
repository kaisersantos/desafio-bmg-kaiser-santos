using AutoMapper;
using Bmg.Domain;
using Bmg.Application.Services.Carts.Models;

namespace Bmg.Application.Mappings;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<AddCartItemRequest, CartItemEntity>();

        CreateMap<CartEntity, GetCurrentResponse>();
        CreateMap<CartEntity, GetHistoryResponse>();
        CreateMap<PaymentEntity, GetPaymentResponse>();
        CreateMap<CartItemEntity, GetCartItemResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product!.Price));
    }
}
