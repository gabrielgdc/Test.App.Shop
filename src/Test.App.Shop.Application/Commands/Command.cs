using FluentValidation.Results;

namespace Test.App.Shop.Application.Commands;

public abstract class Command
{
	protected ValidationResult ValidationResult { get; set; }

	public ValidationResult GetValidationResult()
	{
		return ValidationResult;
	}

	public abstract bool IsValid();
}