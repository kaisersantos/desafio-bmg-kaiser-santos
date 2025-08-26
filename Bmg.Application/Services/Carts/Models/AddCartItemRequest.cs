namespace Bmg.Application.Services.Carts.Models;

public record AddCartItemRequest(
    Guid ProductId,
    int Quantity
);