using System;
using FluentValidation;
using Test.App.Shop.Application.Commands;

namespace Test.App.Shop.Application.Validations;

public class DeleteUserPaymentMethodCommandValidation : AbstractValidator<DeleteUserPaymentMethodCommand>
{
    public DeleteUserPaymentMethodCommandValidation()
    {
    }

    private void ValidatePaymentMethodId()
    {
        RuleFor(comm => comm.PaymentMethodId)
            .NotNull()
            .NotEmpty()
            .Must(paymentMethodId => !paymentMethodId.Equals(Guid.Empty))
            .WithErrorCode("88")
            .WithMessage("Informe um id de método de pagamento válido");
    }
}
