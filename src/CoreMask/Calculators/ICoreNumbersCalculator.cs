using System.Numerics;
using PMart.DeveloperTools.CoreMask.Models;

namespace PMart.DeveloperTools.CoreMask.Calculators;

/// <summary>
/// The interface for the core numbers calculator.
/// </summary>
public interface ICoreNumbersCalculator
{
    /// <summary>
    /// Calculates the core numbers from the provided input, that should be a core mask in decimal representation.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The result.</returns>
    public CalculatorResult CalculateCoreNumbers(string? input);
    
    /// <summary>
    /// Calculates the core numbers from the provided core mask (number in decimal representation).
    /// </summary>
    /// <param name="coreMaskNumber">The core mask number.</param>
    /// <returns>The result.</returns>
    public CalculatorResult CalculateCoreNumbers(BigInteger coreMaskNumber);
}