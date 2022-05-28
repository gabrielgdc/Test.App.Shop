using System;
using FluentValidation;
using Test.App.Shop.Application.Commands;

namespace Test.App.Shop.Application.Validations;

public class AddUserPaymentMethodCommandValidation : AbstractValidator<AddUserPaymentMethodCommand>
{
    public AddUserPaymentMethodCommandValidation()
    {
        ValidateAlias();
        ValidateCardNumber();
        ValidateExpireDate();
        ValidateCardHolderName();
        ValidateSecurityNumber();
    }

    private void ValidateAlias()
    {
        RuleFor(comm => comm.SecurityNumber)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("88")
            .WithMessage("Informe um nome para o cartão válido");
    }

    private void ValidateCardNumber()
    {
        RuleFor(comm => comm.CardNumber.ToString())
            .NotEmpty()
            .NotNull()
            .CreditCard()
            .WithErrorCode("88")
            .WithMessage("Informe um número de cartão válido");
    }

    private void ValidateExpireDate()
    {
        RuleFor(comm => comm.ExpireDate)
            .NotEmpty()
            .NotNull()
            .Must(expireDate => expireDate > DateTime.UtcNow)
            .WithErrorCode("88")
            .WithMessage("Informe uma data de validade válida no formato mm/yy");
    }

    private void ValidateCardHolderName()
    {
        RuleFor(comm => comm.CardHolderName)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("88")
            .WithMessage("Informe um nome do titular válido");
    }

    private void ValidateSecurityNumber()
    {
        RuleFor(comm => comm.SecurityNumber)
            .NotEmpty()
            .NotNull()
            .Must(securityNumber => securityNumber.ToString().Length.Equals(3))
            .WithErrorCode("88")
            .WithMessage("Informe um SecurityNumber válido");
    }
}
