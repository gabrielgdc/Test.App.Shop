using FluentValidation;

namespace Test.App.Shop.Application.Validations.Custom;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string> Cpf<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CpfValidator<T, string>());
    }
}
