using System.Numerics;

namespace PMart.DeveloperTools.CoreMask.Parsers;

/// <summary>
/// The core mask input parser.
/// </summary>
/// <seealso cref="ICoreNumbersInputParser"/>
internal class CoreMaskInputParser : ICoreMaskInputParser
{
    /// <inheritdoc />
    public BigInteger? ParseInputToCoreMask(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        if (!BigInteger.TryParse(input.Trim(), out var coreMask))
        {
            return null;
        }
        
        return coreMask;
    }
}