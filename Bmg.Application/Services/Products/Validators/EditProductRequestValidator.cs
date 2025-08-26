using Bmg.Application.Services.Products.Models;
using FluentValidation;

namespace Bmg.Application.Services.Products.Validators;

public class EditProductRequestValidator : AbstractValidator<EditProductRequest>
{
    public EditProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo {MaxLength} caracteres.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");
    }
}
