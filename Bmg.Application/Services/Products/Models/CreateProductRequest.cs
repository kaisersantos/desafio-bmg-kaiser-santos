namespace Bmg.Application.Services.Products.Models;

public record CreateProductRequest(
    string Name,
    decimal Price,
    int InitialStock
)
{
    public string Name { get; init; } = Name?.Trim() ?? string.Empty;
};