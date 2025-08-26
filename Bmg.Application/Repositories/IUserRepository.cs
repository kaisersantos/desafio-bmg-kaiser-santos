using Bmg.Domain;

namespace Bmg.Application.Repositories;

public interface IUserRepository
{
    Task<UserEntity> AddAsync(UserEntity user);
    Task<UserEntity?> GetByIdAsync(Guid id);
    Task<UserEntity?> GetByEmailAsync(string email);
}