using PMart.CoreMask.Models;

namespace PMart.CoreMask.Calculators;

/// <summary>
/// The interface for the core mask calculator.
/// </summary>
public interface ICoreMaskCalculator
{
    /// <summary>
    /// Calculates the core mask from the provided input, that should be a collection of core numbers (e.g "1,2-4,5").
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The result.</returns>
    public CalculatorResult CalculateCoreMask(string? input);
}