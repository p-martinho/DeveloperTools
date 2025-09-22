using System.Globalization;
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

        var isHexFormat = input.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase);

        return isHexFormat ? ParseInputInHexFormat(input) : ParseInputInDecimalFormat(input);
    }

    private static BigInteger? ParseInputInDecimalFormat(string input)
    {
        if (!BigInteger.TryParse(input.AsSpan().Trim(), out var coreMask))
        {
            return null;
        }

        return coreMask;
    }

    private static BigInteger? ParseInputInHexFormat(string input)
    {
        // Remove the '0x' prefix and add a trailing zero, to the number be interpreted as positive
        var inputInHex = $"0{input.AsSpan().Slice(2).Trim().ToString()}";

        if (!BigInteger.TryParse(inputInHex, NumberStyles.HexNumber, null, out var coreMask))
        {
            return null;
        }

        return coreMask;
    }
}