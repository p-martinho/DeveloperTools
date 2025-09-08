using PMart.DeveloperTools.CoreMask.Parsers;

namespace CoreMask.Tests.Parsers;

public class CoreNumbersInputParserTests
{
    private readonly CoreNumbersInputParser _inputParser = new();

    [Theory]
    [MemberData(nameof(SuccessTheoryData))]
    public void ParseInputToCoreMask_ShouldSucceed(string input, ushort[] expectedCoreNumbers)
    {
        // Arrange

        // Act
        var result = _inputParser.ParseInputToCoreNumbers(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCoreNumbers.Length, result.Count);
        for (var i = 0; i < expectedCoreNumbers.Length; i++)
        {
            Assert.Equal(expectedCoreNumbers[i], result[i]);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("A")]
    [InlineData("1-a")]
    [InlineData("1-")]
    [InlineData("-1")]
    [InlineData(",1")]
    [InlineData("1,")]
    [InlineData("0,,2")]
    [InlineData("0--2")]
    [InlineData("0-1-2")]
    public void ParseInputToCoreMask_WhenInvalidInput_ShouldReturnNull(string? input)
    {
        // Arrange

        // Act
        var result = _inputParser.ParseInputToCoreNumbers(input!);

        // Assert
        Assert.Null(result);
    }

    public static IEnumerable<TheoryDataRow<string, ushort[]>> SuccessTheoryData =>
    [
        new("0", [0]),
        new("1", [1]),
        new("0,1", [0, 1]),
        new(" 0 , 1 ", [0, 1]),
        new("1,2,3", [1, 2, 3]),
        new("1-3", [1, 2, 3]),
        new(" 1 - 3 ", [1, 2, 3]),
        new("0-1", [0, 1]),
        new("1-1", [1]),
        new("0,2,4-6,10", [0, 2, 4, 5, 6, 10]),
        new("0, 2, 4 - 6, 10", [0, 2, 4, 5, 6, 10]),
        new("255", [255]),
        new("1,0,4-2", [1, 0, 4, 3, 2]), // unordered
        new("5,2,4-5,0,0", [5, 2, 4, 5, 0, 0]) // unordered, and with duplicates
    ];
}