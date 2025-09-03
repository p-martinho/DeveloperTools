using System.Text.RegularExpressions;

namespace PMart.CoreMask.Validators;

/// <summary>
/// The core mask input validator.
/// </summary>
/// <seealso cref="ICoreMaskInputValidator"/>
internal partial class CoreMaskInputValidator : ICoreMaskInputValidator
{
    private const string RegexPattern = "^[0-9]+$";
    private const string EmptyInputErrorMessage = "Error: Input should not be empty";
    private const string InvalidInputErrorMessage = "Error: The input is invalid, it should include only numbers";
    
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