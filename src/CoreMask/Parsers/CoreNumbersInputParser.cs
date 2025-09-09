namespace PMart.DeveloperTools.CoreMask.Parsers;

/// <summary>
/// The core numbers input parser.
/// </summary>
/// <seealso cref="ICoreNumbersInputParser"/>
internal class CoreNumbersInputParser : ICoreNumbersInputParser
{
    /// <inheritdoc />
    public IList<ushort>? ParseInputToCoreNumbers(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        var cores = new List<ushort>();

        var inputSpan = input.AsSpan();

        var coresSubstrings = inputSpan.Split(',');

        foreach (var segment in coresSubstrings)
        {
            var parsedCores = ParseSubstring(inputSpan[segment]);

            if (parsedCores is null)
            {
                return null;
            }

            cores.AddRange(parsedCores);
        }

        return cores;
    }

    private static List<ushort>? ParseSubstring(ReadOnlySpan<char> substringSpan)
    {
        if (substringSpan.IsEmpty || substringSpan.IsWhiteSpace())
        {
            return null;
        }

        var hyphensCount = substringSpan.Count('-');

        if (hyphensCount > 1)
        {
            return null;
        }

        if (hyphensCount == 1)
        {
            return ParseGroup(substringSpan);
        }

        return ushort.TryParse(substringSpan, out var core) ? [core] : null;
    }

    private static List<ushort>? ParseGroup(ReadOnlySpan<char> groupSpan)
    {
        var cores = new List<ushort>();

        // Parse the first number (before '-')
        if (!ushort.TryParse(groupSpan[..groupSpan.IndexOf('-')], out var firstCore))
        {
            return null;
        }

        // Parse the second number (after '-')
        if (!ushort.TryParse(groupSpan[(groupSpan.LastIndexOf('-') + 1)..], out var lastCore))
        {
            return null;
        }

        var minCore = Math.Min(firstCore, lastCore);
        var maxCore = Math.Max(firstCore, lastCore);

        for (var c = minCore; c <= maxCore; c++)
        {
            cores.Add(c);
        }

        if (minCore != firstCore)
        {
            // Keep it unordered
            cores.Reverse();
        }

        return cores;
    }
}