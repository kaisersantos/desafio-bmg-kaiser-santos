using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bmg.Application.Repositories;
using Bmg.Adapter.Infra.EFCore.Repositories;

namespace Bmg.Adapter.Infra.EFCore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        var dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "bmg.db");

        services.AddDbContext<BmgDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        return services;
    }
}
