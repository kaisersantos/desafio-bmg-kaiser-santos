using Bmg.Domain;
using Bmg.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Bmg.Adapter.Infra.EFCore.Repositories.Base;

namespace Bmg.Adapter.Infra.EFCore.Repositories;

public class UserRepository(BmgDbContext context) : AuditableRepository<UserEntity>(context), IUserRepository
{
    public async Task<UserEntity?> GetByIdAsync(Guid id) =>
        await _context.Users
            .Where(p => p.DeletedAt == null)
            .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<UserEntity?> GetByEmailAsync(string email) =>
        await _context.Users
            .Where(p => p.DeletedAt == null)
            .FirstOrDefaultAsync(u => u.Email == email);
}
