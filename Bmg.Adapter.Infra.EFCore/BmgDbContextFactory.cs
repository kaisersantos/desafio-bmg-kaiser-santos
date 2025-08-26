using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bmg.Adapter.Infra.EFCore
{
    public class BmgDbContextFactory : IDesignTimeDbContextFactory<BmgDbContext>
    {
        public BmgDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BmgDbContext>();

            var dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "bmg.db");
            var folder = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new BmgDbContext(optionsBuilder.Options);
        }
    }
}
