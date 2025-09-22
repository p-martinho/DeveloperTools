using System.Numerics;
using PMart.DeveloperTools.CoreMask.Parsers;

namespace CoreMask.Tests.Parsers;

public class CoreMaskInputParserTests
{
    private readonly CoreMaskInputParser _inputParser = new();

    [Theory]
    [MemberData(nameof(SuccessTheoryData))]
    public void ParseInputToCoreMask_ShouldSucceed(string input, BigInteger expectedCoreMask)
    {
        // Arrange

        // Act
        var result = _inputParser.ParseInputToCoreMask(input);

        // Assert
        Assert.Equal(expectedCoreMask, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("A")]
    [InlineData("0x")]
    public void ParseInputToCoreMask_WhenInvalidInput_ShouldReturnNull(string? input)
    {
        // Arrange

        // Act
        var result = _inputParser.ParseInputToCoreMask(input!);

        // Assert
        Assert.Null(result);
    }

    public static IEnumerable<TheoryDataRow<string, BigInteger>> SuccessTheoryData =>
    [
        new("-1", -1),
        new("0", 0),
        new("1", 1),
        new("10", 10),
        new((BigInteger.One << 255).ToString(), BigInteger.One << 255),
        new(" 1 ", 1),
        new("0xA", 10),
        new("0Xa", 10),
        new("0xAA", 170)
    ];
}