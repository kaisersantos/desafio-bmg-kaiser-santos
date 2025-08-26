namespace Bmg.Application.Services.Carts.Models;

public record GetCartItemResponse
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get => UnitPrice * Quantity; }
};