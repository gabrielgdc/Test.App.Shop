using MediatR;
using Test.App.Shop.Application.Dtos;
using Test.App.Shop.Application.Validations;

namespace Test.App.Shop.Application.Commands;

public class LoginUserCommand : Command, IRequest<UserLoggedInDto>
{
    public string Cpf { get; }
    public string Password { get; }

    public LoginUserCommand(string cpf, string password)
    {
        Cpf = cpf;
        Password = password;
    }

    public override bool IsValid()
    {
        ValidationResult = new LoginUserCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
