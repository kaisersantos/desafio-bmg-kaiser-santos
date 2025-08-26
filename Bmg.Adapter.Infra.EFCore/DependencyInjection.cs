using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bmg.Adapter.Infra.EFCore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        var dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "bmg.db");

        services.AddDbContext<BmgDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        return services;
    }
}
