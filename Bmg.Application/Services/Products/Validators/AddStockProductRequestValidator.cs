using Bmg.Application.Services.Products.Models;
using FluentValidation;

namespace Bmg.Application.Services.Products.Validators;

public class AddStockProductRequestValidator : AbstractValidator<AddStockProductRequest>
{
    public AddStockProductRequestValidator()
    {
        RuleFor(p => p.Quantity)
            .GreaterThan(0).WithMessage("O estoque deve ser maior que zero.");
    }
}
