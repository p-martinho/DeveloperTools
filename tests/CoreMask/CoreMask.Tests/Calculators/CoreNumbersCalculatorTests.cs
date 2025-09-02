using System.Numerics;
using NSubstitute;
using PMart.CoreMask.Calculators;
using PMart.CoreMask.Constants;
using PMart.CoreMask.Parsers;
using PMart.CoreMask.Validators;

namespace PMart.CoreMask.Tests.Calculators;

public class CoreNumbersCalculatorTests
{
    private const string ParsingErrorMessage = "Error: not able to parse the input (invalid number?)";
    private const string InvalidNumberErrorMessage = "Error: the number is invalid (must be an integer higher than 0)";
    private const string MaxCoreNumberErrorMessage = "Error: core number higher than the maximum supported";

    private readonly CoreNumbersCalculator _calculator;
    private readonly ICoreMaskInputValidator _inputValidator;
    private readonly ICoreMaskInputParser _inputParser;

    public CoreNumbersCalculatorTests()
    {
        _inputValidator = Substitute.For<ICoreMaskInputValidator>();

        _inputParser = Substitute.For<ICoreMaskInputParser>();

        _calculator = new CoreNumbersCalculator(_inputValidator, _inputParser);
    }

    [Theory]
    [MemberData(nameof(SuccessTheoryData))]
    public void CalculateCoreMask_ShouldSucceed(BigInteger coreMask, ushort[] expectedCoreNumbers)
    {
        // Arrange
        const string input = "input";
        _inputValidator.ValidateInput(input).Returns((string?)null);
        _inputParser.ParseInputToCoreMask(input).Returns(coreMask);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Null(result.ErrorMessage);
        var actualCoreNumbers = result.CoreNumbers.ToArray();
        Assert.Equal(expectedCoreNumbers.Length, actualCoreNumbers.Length);
        for (var i = 0; i < expectedCoreNumbers.Length; i++)
        {
            Assert.Equal(expectedCoreNumbers[i], actualCoreNumbers[i]);
        }
        Assert.NotNull(result.CoreMask);
        Assert.Equal(coreMask, result.CoreMask.CoreMaskNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData("someError")]
    public void CalculateCoreMask_WhenInputValidationFails_ShouldFail(string errorMessage)
    {
        // Arrange
        const string input = "input";
        _inputValidator.ValidateInput(input).Returns(errorMessage);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Equal(errorMessage, result.ErrorMessage);
        Assert.Empty(result.CoreNumbers);
        Assert.Null(result.CoreMask);
    }

    [Fact]
    public void CalculateCoreMask_WhenInputParsingFails_ShouldFail()
    {
        // Arrange
        const string input = "input";
        _inputValidator.ValidateInput(null).ReturnsForAnyArgs((string?)null);
        _inputParser.ParseInputToCoreMask(input).Returns((BigInteger?)null);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Equal(ParsingErrorMessage, result.ErrorMessage);
        Assert.Empty(result.CoreNumbers);
        Assert.Null(result.CoreMask);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CalculateCoreMask_WhenInvalidNumber_ShouldFail(BigInteger coreMask)
    {
        // Arrange
        const string input = "input";
        _inputValidator.ValidateInput(null).ReturnsForAnyArgs((string?)null);
        _inputParser.ParseInputToCoreMask(input).Returns(coreMask);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Contains(InvalidNumberErrorMessage, result.ErrorMessage);
        Assert.Empty(result.CoreNumbers);
        Assert.Null(result.CoreMask);
    }

    [Fact]
    public void CalculateCoreMask_WhenMaxCoreNumberIsExceeded_ShouldFail()
    {
        // Arrange
        const string input = "input";
        _inputValidator.ValidateInput(null).ReturnsForAnyArgs((string?)null);
        _inputParser.ParseInputToCoreMask(input).Returns(BigInteger.One << CoreMaskConstants.MaxCoreNumber + 1);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Contains(MaxCoreNumberErrorMessage, result.ErrorMessage);
        Assert.Empty(result.CoreNumbers);
        Assert.Null(result.CoreMask);
    }

    public static IEnumerable<TheoryDataRow<BigInteger, ushort[]>> SuccessTheoryData =>
    [
        new(1, [0]), // Bin: 1
        new(3, [0, 1]), // Bin: 11
        new(14, [1, 2, 3]), // Bin: 1110
        new(53, [0, 2, 4, 5]), // Bin: 110101
        new(BigInteger.One << 255, [255]) // 256 bits, first bit enabled
    ];
}