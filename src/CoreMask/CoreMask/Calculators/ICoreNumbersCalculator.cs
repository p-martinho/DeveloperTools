using PMart.CoreMask.Models;

namespace PMart.CoreMask.Calculators;

/// <summary>
/// The interface for the core numbers calculator.
/// </summary>
public interface ICoreNumbersCalculator
{
    /// <summary>
    /// Calculates the core numbers from the provided input, that should be a core mask in decimal format.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The result.</returns>
    public CalculatorResult CalculateCoreMask(string? input);
}