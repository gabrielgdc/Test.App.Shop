using System;
using System.Linq;
using FluentValidation;
using Test.App.Shop.Application.Commands;
using Test.App.Shop.Application.Validations.Custom;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Application.Validations;

public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidation()
    {
        ValidateCpf();
        ValidateFullName();
        ValidateAddress();
        ValidatePassword();
        ValidateBirthDate();
        ValidateGenderId();
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

    private void ValidateFullName()
    {
        RuleFor(comm => comm.FullName)
            .NotEmpty()
            .NotNull()
            .Must(fullName => fullName.Split(" ").Length > 1)
            .WithMessage("Informe o nome completo")
            .WithErrorCode("88");
    }

    private void ValidateAddress()
    {
        RuleFor(comm => comm.Address.City)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe uma cidade válida")
            .WithErrorCode("88");

        RuleFor(comm => comm.Address.Complement)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe um complemento válido")
            .WithErrorCode("88");

        RuleFor(comm => comm.Address.Country)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe um país válido")
            .WithErrorCode("88");

        RuleFor(comm => comm.Address.District)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe um bairro válido")
            .WithErrorCode("88");

        RuleFor(comm => comm.Address.Number)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe um número válido")
            .WithErrorCode("88");

        RuleFor(comm => comm.Address.State)
            .NotEmpty()
            .NotNull()
            .Length(2)
            .WithMessage("Informe um estado válido no padrão UF")
            .WithErrorCode("88");

        RuleFor(comm => comm.Address.Street)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe uma rua válida")
            .WithErrorCode("88");

        RuleFor(comm => comm.Address.Street)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe uma rua válida")
            .WithErrorCode("88");

        RuleFor(comm => comm.Address.ZipCode)
            .NotEmpty()
            .NotNull()
            .Must(zipCode => new string(zipCode.Where(char.IsDigit).ToArray()).Length.Equals(8))
            .WithMessage("Informe um CPE válido de 8 dígitos")
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

    private void ValidateBirthDate()
    {
        RuleFor(comm => comm.BirthDate)
            .NotEmpty()
            .NotNull()
            .Must(birthDate => birthDate < DateTime.UtcNow)
            .WithMessage("Informe uma data de nascimento válida")
            .WithErrorCode("88");
    }

    private void ValidateGenderId()
    {
        RuleFor(comm => comm.GenderId)
            .NotEmpty()
            .NotNull()
            .Must(genderId => Enumeration.GetAll<UserGender>().Any(gender => gender.Id.Equals(genderId)))
            .WithMessage($"Informe um gênero válido entre os valores {string.Join(", ", Enumeration.GetAll<UserGender>())}")
            .WithErrorCode("88");
    }
}
