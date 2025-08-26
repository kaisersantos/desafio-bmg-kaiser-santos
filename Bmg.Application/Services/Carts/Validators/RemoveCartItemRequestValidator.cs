using Bmg.Application.Services.Carts.Models;
using FluentValidation;

namespace Bmg.Application.Services.Carts.Validators;

public class RemoveCartItemRequestValidator : AbstractValidator<RemoveCartItemRequest>
{
    public RemoveCartItemRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
    }
}
