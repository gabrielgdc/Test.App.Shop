using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Test.App.Shop.Application.Validations.Custom;

public class CpfValidator<T, TProperty> : PropertyValidator<T, string>
{
    private const int ValidLength = 11;
    private static IEnumerable<int> FirstMultiplierCollection => new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    private static IEnumerable<int> SecondMultiplierCollection => new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    public override string Name => "Cpf Validator";

    public CpfValidator()
    {
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        value = Regex.Replace(value, "[^a-zA-Z0-9]", "");

        if (IsValidLength(value) || AllDigitsAreEqual(value) || string.IsNullOrEmpty(value)) return false;

        var cpf = value.Select(x => (int)char.GetNumericValue(x)).ToArray();
        var digits = GetDigits(cpf);

        return value.EndsWith(digits);
    }

    private static bool AllDigitsAreEqual(string value) => value.All(valueChar => valueChar == value.FirstOrDefault());

    private static bool IsValidLength(string value) => !string.IsNullOrWhiteSpace(value) && value.Length != ValidLength;

    private static string GetDigits(IReadOnlyList<int> cpf)
    {
        var first = CalculateValue(FirstMultiplierCollection, cpf);
        var second = CalculateValue(SecondMultiplierCollection, cpf);

        return $"{CalculateDigit(first)}{CalculateDigit(second)}";
    }

    private static int CalculateValue(IEnumerable<int> weight, IReadOnlyList<int> numbers)
    {
        return weight.Select((t, i) => t * numbers[i]).Sum();
    }

    private static int CalculateDigit(int sum)
    {
        var modResult = (sum % 11);
        return modResult < 2 ? 0 : 11 - modResult;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "Informe um CPF válido";
}
