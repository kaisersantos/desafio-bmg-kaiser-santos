namespace Bmg.Application.Services.Products.Models;

public record EditProductRequest(
    string Name,
    decimal Price
)
{
    public string Name { get; init; } = Name?.Trim() ?? string.Empty;
};