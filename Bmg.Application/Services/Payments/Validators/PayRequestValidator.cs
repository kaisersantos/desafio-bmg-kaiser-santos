using Bmg.Application.Services.Payments.Models;
using FluentValidation;

namespace Bmg.Application.Services.Payments.Validators;

public class PayRequestValidator : AbstractValidator<PayRequest>
{
    public PayRequestValidator()
    {
        RuleFor(x => x.Method)
            .IsInEnum().WithMessage("Método de pagamento inválido.");
    }
}
