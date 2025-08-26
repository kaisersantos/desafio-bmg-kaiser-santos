using Microsoft.EntityFrameworkCore;

namespace Bmg.Adapter.Infra.EFCore.Repositories.Base;

public class NonAuditableRepository<TEntity>(BmgDbContext context) : BaseRepository<TEntity>(context) where TEntity : class
{
    public virtual async Task HardDeleteAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }
}
