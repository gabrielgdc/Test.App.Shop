using FluentValidation;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Application.Validations.Custom;

namespace Test.App.Shop.Application.Validations;

public class LoginUserCommandValidation : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidation()
    {
        ValidateCpf();
        ValidatePassword();
    }

    private void ValidateCpf()
    {
        RuleFor(comm => comm.Cpf)
            .NotEmpty()
            .NotNull()
            .Cpf()
            .WithMessage("Informe um cpf válido")
            .WithErrorCode("88");
    }

    private void ValidatePassword()
    {
        RuleFor(comm => comm.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe uma senha válida")
            .WithErrorCode("88");
    }
}
