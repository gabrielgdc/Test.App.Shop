using System.Linq;
using FluentValidation;
using Test.App.Shop.Application.Commands;

namespace Test.App.Shop.Application.Validations;

public class SendNewOrderCommandValidation : AbstractValidator<SendNewOrderCommand>
{
    public SendNewOrderCommandValidation()
    {
        ValidateApplicationsIds();
        ValidateUserId();
        ValidatePaymentId();
    }

    private void ValidateApplicationsIds()
    {
        RuleFor(comm => comm.CartProductsIds)
            .NotEmpty()
            .NotNull()
            .Must(applicationsIds => applicationsIds.Any())
            .WithErrorCode("88")
            .WithMessage("Informe alguma aplicação para efetuar a ordem");
    }

    private void ValidateUserId()
    {
        RuleFor(comm => comm.UserId)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("88")
            .WithMessage("Informe um válido");
    }

    private void ValidatePaymentId()
    {
        RuleFor(comm => comm.PaymentId)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("88")
            .WithMessage("Informe um válido");
    }
}
