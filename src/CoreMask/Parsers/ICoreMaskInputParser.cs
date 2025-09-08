using System.Numerics;

namespace PMart.DeveloperTools.CoreMask.Parsers;

/// <summary>
/// The interface for the core mask input parser.
/// </summary>
internal interface ICoreMaskInputParser
{
    /// <summary>
    /// Parses the input to a number representing the core mask.
    /// </summary>
    /// <param name="input">The input to parse.</param>
    /// <returns>The number representing the core mask. In case of error parsing the input, it returns <c>null</c>.</returns>
    public BigInteger? ParseInputToCoreMask(string input);
}