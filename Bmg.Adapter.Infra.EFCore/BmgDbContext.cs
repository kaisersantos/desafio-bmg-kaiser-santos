using Microsoft.EntityFrameworkCore;
using Bmg.Domain;
using System.Reflection;

namespace Bmg.Adapter.Infra.EFCore;

public class BmgDbContext(DbContextOptions<BmgDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}