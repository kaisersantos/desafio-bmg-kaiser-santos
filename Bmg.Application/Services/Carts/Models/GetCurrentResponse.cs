namespace Bmg.Application.Services.Carts.Models;

public record GetCurrentResponse
{
    public Guid Id { get; set; }
    public IEnumerable<GetCartItemResponse> Items { get; set; } = [];
    public decimal TotalPrice { get => Items.Sum(item => item.Price); }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
};