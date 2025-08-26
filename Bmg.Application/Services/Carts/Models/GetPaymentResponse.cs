using Bmg.Domain;

namespace Bmg.Application.Services.Carts.Models;

public record GetPaymentResponse
{
    public PaymentStatus Status { get; set; }
    public PaymentMethod Method { get; set; }
};