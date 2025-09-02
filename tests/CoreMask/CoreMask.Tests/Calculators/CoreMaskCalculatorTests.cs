using System.Numerics;
using NSubstitute;
using PMart.CoreMask.Calculators;
using PMart.CoreMask.Constants;
using PMart.CoreMask.Parsers;
using PMart.CoreMask.Validators;

namespace PMart.CoreMask.Tests.Calculators;

public class CoreMaskCalculatorTests
{
    private const string ParsingErrorMessage = "Error: not able to parse the input (invalid numbers?)";
    private const string MaxNumberOfCoresErrorMessage = "Error: the number of cores is more than the maximum supported";
    private const string MaxCoreNumberErrorMessage = "Error: core number higher than the maximum supported";

    private readonly CoreMaskCalculator _calculator;
    private readonly ICoreNumbersInputValidator _inputValidator;
    private readonly ICoreNumbersInputParser _inputParser;

    public CoreMaskCalculatorTests()
    {
        _inputValidator = Substitute.For<ICoreNumbersInputValidator>();

        _inputParser = Substitute.For<ICoreNumbersInputParser>();

        _calculator = new CoreMaskCalculator(_inputValidator, _inputParser);
    }

    [Theory]
    [MemberData(nameof(SuccessTheoryData))]
    public void CalculateCoreMask_ShouldSucceed(ushort[] coreNumbers, BigInteger expectedCoreMask)
    {
        // Arrange
        const string input = "input";
        _inputValidator.ValidateInput(input).Returns((string?)null);
        _inputParser.ParseInputToCoreNumbers(input).Returns(coreNumbers);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Null(result.ErrorMessage);
        var expectedCoreNumbers = coreNumbers.Distinct().OrderBy(c => c).ToArray();
        var actualCoreNumbers = result.CoreNumbers.ToArray();
        Assert.Equal(expectedCoreNumbers.Length, actualCoreNumbers.Length);
        for (var i = 0; i < expectedCoreNumbers.Length; i++)
        {
            Assert.Equal(expectedCoreNumbers[i], actualCoreNumbers[i]);
        }
        Assert.NotNull(result.CoreMask);
        Assert.Equal(expectedCoreMask, result.CoreMask.CoreMaskNumber);
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
        _inputParser.ParseInputToCoreNumbers(input).Returns((IList<ushort>?)null);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Equal(ParsingErrorMessage, result.ErrorMessage);
        Assert.Empty(result.CoreNumbers);
        Assert.Null(result.CoreMask);
    }

    [Fact]
    public void CalculateCoreMask_WhenMaxNumberOfCoresIsExceeded_ShouldFail()
    {
        // Arrange
        const string input = "input";
        _inputValidator.ValidateInput(null).ReturnsForAnyArgs((string?)null);
        var coreNumbers = Enumerable.Range(0, CoreMaskConstants.MaxNumberOfCores + 1).Select(n => (ushort)n).ToList();
        _inputParser.ParseInputToCoreNumbers(input).Returns(coreNumbers);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Contains(MaxNumberOfCoresErrorMessage, result.ErrorMessage);
        Assert.Empty(result.CoreNumbers);
        Assert.Null(result.CoreMask);
    }

    [Fact]
    public void CalculateCoreMask_WhenMaxCoreNumberIsExceeded_ShouldFail()
    {
        // Arrange
        const string input = "input";
        _inputValidator.ValidateInput(null).ReturnsForAnyArgs((string?)null);
        var coreNumbers = new List<ushort> { CoreMaskConstants.MaxCoreNumber + 1 };
        _inputParser.ParseInputToCoreNumbers(input).Returns(coreNumbers);

        // Act
        var result = _calculator.CalculateCoreMask(input);

        // Assert
        Assert.Contains(MaxCoreNumberErrorMessage, result.ErrorMessage);
        Assert.Empty(result.CoreNumbers);
        Assert.Null(result.CoreMask);
    }

    public static IEnumerable<TheoryDataRow<ushort[], BigInteger>> SuccessTheoryData =>
    [
        new([0], 1), // Bin: 1
        new([0, 1], 3), // Bin: 11
        new([1, 2, 3], 14), // Bin: 1110
        new([0, 2, 4, 5], 53), // Bin: 110101
        new([255], BigInteger.One << 255), // 256 bits, first bit enabled
        new([5, 2, 4, 5, 0, 0], 53)  // unordered, and with duplicates
    ];
}