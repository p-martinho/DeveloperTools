using PMart.DeveloperTools.CoreMask.Models;

namespace PMart.DeveloperTools.CoreMask.Calculators;

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
    
    /// <summary>
    /// Calculates the core mask from the provided core numbers.
    /// </summary>
    /// <param name="coreNumbers">The core numbers to add to the mask.</param>
    /// <returns>The result.</returns>
    public CalculatorResult CalculateCoreMask(IEnumerable<ushort> coreNumbers);
}