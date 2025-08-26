using Bmg.Application.Services.Payments.Models;
using FluentValidation;

namespace Bmg.Application.Services.Payments.Validators;

public class PayByPixRequestValidator : AbstractValidator<PayByPixRequest>
{
    public PayByPixRequestValidator()
    {
        Include(new PayRequestValidator());

        RuleFor(x => x.PixKey)
            .NotEmpty().WithMessage("A chave PIX é obrigatória.")
            .MaximumLength(200).WithMessage("A chave PIX não pode ter mais que {MaxLength} caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("A descrição não pode ter mais que {MaxLength} caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
