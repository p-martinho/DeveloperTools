using System.Text.RegularExpressions;

namespace PMart.CoreMask.Validators;

/// <summary>
/// The core numbers input validator.
/// </summary>
/// <seealso cref="ICoreNumbersInputValidator"/>
internal partial class CoreNumbersInputValidator : ICoreNumbersInputValidator
{
    private const string RegexPattern = "^[0-9]+(?:[,-][0-9]+)*$";
    private const string EmptyInputErrorMessage = "Error: Input should not be empty";
    private const string InvalidInputErrorMessage =
        "Error: The input is invalid, it should include only numbers, ',' or '-' (example: 1-3,6,8-10)";

    /// <inheritdoc />
    public string? ValidateInput(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return EmptyInputErrorMessage;
        }

        if (!GetValidationRegex().IsMatch(input))
        {
            return InvalidInputErrorMessage;
        }

        return null;
    }

    [GeneratedRegex(RegexPattern)]
    private partial Regex GetValidationRegex();
}