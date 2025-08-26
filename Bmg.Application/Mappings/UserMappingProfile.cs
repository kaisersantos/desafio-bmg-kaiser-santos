using AutoMapper;
using Bmg.Domain;
using Bmg.Application.Services.Users.Models;

namespace Bmg.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, UserEntity>();
        CreateMap<UserEntity, CreatedUserResponse>();
        CreateMap<UserEntity, VerifyCredentialsResponse>();
    }
}
