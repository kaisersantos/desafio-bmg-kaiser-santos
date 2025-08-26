using Bmg.Application.Services.Users.Models;
using FluentValidation;

namespace Bmg.Application.Services.Users.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail deve ser válido.")
            .MaximumLength(100).WithMessage("O e-mail deve ter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.Password)
        .SetValidator(new PasswordValidator());
    }
}
