using Microsoft.Extensions.DependencyInjection;
using Bmg.Application.Services.Users;
using Bmg.Application.Services.Products;
using Bmg.Application.Services.Carts;
using Bmg.Application.Services.Payments;
using Bmg.Application.Services.Jwt;
using FluentValidation;
using Bmg.Application.Services.Carts.Validators;
using Bmg.Application.Services.Carts.Models;
using Bmg.Application.Services.Payments.Models;
using Bmg.Application.Services.Payments.Validators;
using Bmg.Application.Services.Products.Models;
using Bmg.Application.Services.Products.Validators;
using Bmg.Application.Services.Users.Models;
using Bmg.Application.Services.Users.Validators;

namespace Bmg.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddCartItemRequest>, AddCartItemRequestValidator>();
        services.AddScoped<IValidator<RemoveCartItemRequest>, RemoveCartItemRequestValidator>();
        services.AddScoped<IValidator<PayRequest>, PayRequestValidator>();
        services.AddScoped<IValidator<PayByCreditCardRequest>, PayByCreditCardRequestValidator>();
        services.AddScoped<IValidator<PayByPixRequest>, PayByPixRequestValidator>();
        services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
        services.AddScoped<IValidator<AddStockProductRequest>, AddStockProductRequestValidator>();
        services.AddScoped<IValidator<EditProductRequest>, EditProductRequestValidator>();
        services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
        services.AddScoped<IValidator<string>, PasswordValidator>();

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}
