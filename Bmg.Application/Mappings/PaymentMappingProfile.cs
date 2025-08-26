using AutoMapper;
using Bmg.Application.Integrations.Payment.Models;
using Bmg.Application.Services.Payments.Models;

namespace Bmg.Application.Mappings;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<PayByCreditCardRequest, CreditCardPaymentRequest>();
        CreateMap<PayByPixRequest, PixPaymentRequest>();
    }
}
