namespace Bmg.Application.Services.Products.Models;

public record GetProductResponse(
    Guid Id,
    string Name,
    decimal Price,
    int Stock
);