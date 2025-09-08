namespace PMart.DeveloperTools.CoreMask.Parsers;

/// <summary>
/// The interface for the core numbers input parser.
/// </summary>
internal interface ICoreNumbersInputParser
{
    /// <summary>
    /// Parses the input to a collection of core numbers.
    /// </summary>
    /// <param name="input">The input to parse.</param>
    /// <returns>The collection with the core numbers parsed. In case of error parsing the input, it returns <c>null</c>.</returns>
    public IList<ushort>? ParseInputToCoreNumbers(string input);
}