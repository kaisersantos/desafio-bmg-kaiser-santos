using Bmg.Application.Repositories;

namespace Bmg.Adapter.Infra.EFCore.Repositories;

public class UnitOfWork(BmgDbContext context) : IUnitOfWork
{
    private readonly BmgDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await action();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }
}
