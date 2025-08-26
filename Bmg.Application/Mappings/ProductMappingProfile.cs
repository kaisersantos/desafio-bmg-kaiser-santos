using AutoMapper;
using Bmg.Domain;
using Bmg.Application.Services.Products.Models;

namespace Bmg.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductRequest, ProductEntity>()
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.InitialStock));
        CreateMap<ProductEntity, CreatedProductResponse>()
            .ForMember(dest => dest.InitialStock, opt => opt.MapFrom(src => src.Stock));

        CreateMap<AddStockProductRequest, ProductEntity>()
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Quantity));
        CreateMap<EditProductRequest, ProductEntity>();
        CreateMap<ProductEntity, GetProductResponse>();
    }
}
