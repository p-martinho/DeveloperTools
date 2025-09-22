using System.Text.RegularExpressions;

namespace PMart.DeveloperTools.CoreMask.Validators;

/// <summary>
/// The core mask input validator.
/// </summary>
/// <seealso cref="ICoreMaskInputValidator"/>
internal partial class CoreMaskInputValidator : ICoreMaskInputValidator
{
    private const string RegexPattern = "^[0-9]+$|^0x[0-9a-f]+$";
    private const string EmptyInputErrorMessage = "Error: Input should not be empty";
    private const string InvalidInputErrorMessage =
        "Error: The input is invalid, it should include only numbers (and letters A to F in case of hexadecimal format, with prefix '0x')";

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

    [GeneratedRegex(RegexPattern, RegexOptions.IgnoreCase)]
    private partial Regex GetValidationRegex();
}