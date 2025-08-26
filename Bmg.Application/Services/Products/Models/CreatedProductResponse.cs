namespace Bmg.Application.Services.Products.Models;

public record CreatedProductResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int InitialStock { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
};