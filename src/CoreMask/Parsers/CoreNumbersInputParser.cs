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

        var coresSubstrings = input.Split(',');

        foreach (var substring in coresSubstrings)
        {
            var parsedCores = ParseSubstring(substring);

            if (parsedCores is null)
            {
                return null;
            }
            
            cores.AddRange(parsedCores);
        }
        
        return cores;
    }

    private static List<ushort>? ParseSubstring(string substring)
    {
        if (string.IsNullOrWhiteSpace(substring))
        {
            return null;
        }

        var hyphensCount = substring.Count(s => s == '-');

        if (hyphensCount > 1)
        {
            return null;
        }
        
        if (hyphensCount == 1)
        {
            return ParseGroup(substring);
        }
        
        return ushort.TryParse(substring, out var core) ? [core] : null;
    }

    private static List<ushort>? ParseGroup(string group)
    {
        var cores = new List<ushort>();

        var groupSpan = group.AsSpan();

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