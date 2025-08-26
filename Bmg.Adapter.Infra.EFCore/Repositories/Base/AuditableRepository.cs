using Bmg.Domain;

namespace Bmg.Adapter.Infra.EFCore.Repositories.Base;

public class AuditableRepository<TEntity>(BmgDbContext context) : BaseRepository<TEntity>(context) where TEntity : AuditableEntity
{
    public override async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        await base.AddAsync(entity);
        return entity;
    }

    public override async Task UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        await base.UpdateAsync(entity);
    }

    public virtual async Task SoftDeleteAsync(TEntity entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        await base.UpdateAsync(entity);
    }
}
