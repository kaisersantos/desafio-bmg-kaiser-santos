using FluentValidation;

namespace Bmg.Application.Services.Users.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter no mínimo {MinLength} caracteres.")
            .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um dígito.")
            .Matches(@"[!@#$%^&*()_+\-=\[\]{};:'""<>,.?/\\|]").WithMessage("A senha deve conter pelo menos um caractere especial.");
    }
}
