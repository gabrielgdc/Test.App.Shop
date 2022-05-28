using System.Collections;
using FluentValidation;
using FluentValidation.Validators;

namespace Test.App.Shop.Application.Validations.Custom;

public class CreditCardValidator<T> : PropertyValidator<T, string>
{
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        try
        {
            var checkNumbers = new ArrayList();
            var cardNumberLength = value.Length;

            for (var i = cardNumberLength - 2; i >= 0; i -= 2)
            {
                checkNumbers.Add(int.Parse(value[i].ToString()) * 2);
            }

            var checkSum = 0;

            for (var iCount = 0; iCount <= checkNumbers.Count - 1; iCount++)
            {
                var count = 0;

                if ((int)checkNumbers[iCount]! > 9)
                {
                    var numLength = ((int)checkNumbers[iCount]).ToString().Length;
                    for (var x = 0; x < numLength; x++)
                    {
                        count += int.Parse(((int)checkNumbers[iCount]).ToString()[x].ToString());
                    }
                }
                else
                {
                    count = (int)checkNumbers[iCount];
                }

                checkSum += count;
            }

            var originalSum = 0;

            for (var y = cardNumberLength - 1; y >= 0; y -= 2)
            {
                originalSum += int.Parse(value[y].ToString());
            }

            return (originalSum + checkSum) % 10 == 0;
        }
        catch
        {
            return false;
        }
    }

    public override string Name => "Credit Card Validator";
}
