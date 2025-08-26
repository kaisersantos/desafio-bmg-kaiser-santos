using Microsoft.EntityFrameworkCore;

namespace Bmg.Adapter.Infra.EFCore.Repositories.Base;

public class BaseRepository<TEntity>(BmgDbContext context) where TEntity : class
{
    protected readonly BmgDbContext _context = context;

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
