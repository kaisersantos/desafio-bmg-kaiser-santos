namespace Bmg.Application.Services.Carts.Models;

public record GetHistoryResponse
{
    public Guid Id { get; set; }
    public IEnumerable<GetCartItemResponse> Items { get; set; } = [];
    public decimal TotalPrice { get => Items.Sum(item => item.Price); }
    public GetPaymentResponse? Payment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
};