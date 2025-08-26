using Bmg.Application.Services.Payments.Models;
using FluentValidation;

namespace Bmg.Application.Services.Payments.Validators;

public class PayByCreditCardRequestValidator : AbstractValidator<PayByCreditCardRequest>
{
    public PayByCreditCardRequestValidator()
    {
        Include(new PayRequestValidator());

        RuleFor(x => x.CardNumber)
            .CreditCard().WithMessage("Número do cartão inválido.")
            .NotEmpty().WithMessage("O número do cartão é obrigatório.");

        RuleFor(x => x.CardHolder)
            .NotEmpty().WithMessage("O nome do titular é obrigatório.")
            .MaximumLength(200).WithMessage("O nome do titular não pode ter mais que {MaxLength} caracteres.");

        RuleFor(x => x.Expiration)
            .NotEmpty().WithMessage("A data de expiração é obrigatória.")
            .MaximumLength(7).WithMessage("A data de expiração deve ter o formato MM/AAAA.");

        RuleFor(x => x.Cvv)
            .InclusiveBetween((short)100, (short)9999).WithMessage("O CVV deve ter entre 3 e 4 dígitos.");
    }
}
