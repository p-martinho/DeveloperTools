namespace PMart.CoreMask.Validators;

/// <summary>
/// The interface for the core numbers input validator.
/// </summary>
internal interface ICoreNumbersInputValidator
{
    /// <summary>
    /// Validates the input, that should be a collection of core numbers (e.g "1,2-4,5").
    /// </summary>
    /// <param name="input">The input to validate.</param>
    /// <returns><c>null</c> when the input is valid; an error message, otherwise.</returns>
    string? ValidateInput(string? input);
}