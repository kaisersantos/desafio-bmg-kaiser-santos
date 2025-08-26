namespace Bmg.Application.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task ExecuteAsync(Func<Task> action);
}
