namespace PMart.DeveloperTools.CoreMask.Validators;

/// <summary>
/// The interface for the core mask input validator.
/// </summary>
internal interface ICoreMaskInputValidator
{
    /// <summary>
    /// Validates the input, that should be a core mask in decimal representation.
    /// </summary>
    /// <param name="input">The input to validate.</param>
    /// <returns><c>null</c> when the input is valid; an error message, otherwise.</returns>
    string? ValidateInput(string? input);
}