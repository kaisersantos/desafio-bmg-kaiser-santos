using AutoMapper;
using Bmg.Application.Services.Users.Models;
using Bmg.Application.Services.Jwt.Models;
using Bmg.Api.Settings;
using Bmg.Application.Utils;

namespace Bmg.Api.Mappings;

public class JwtMappingProfile : Profile
{
    public JwtMappingProfile()
    {
        CreateMap<JwtSettings, CreateJwtRequest>();
        CreateMap<VerifyCredentialsResponse, CreateJwtRequest>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.Role.GetDescription()));
    }
}
