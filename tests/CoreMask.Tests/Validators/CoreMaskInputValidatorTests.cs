using PMart.DeveloperTools.CoreMask.Validators;

namespace CoreMask.Tests.Validators;

public class CoreMaskInputValidatorTests
{
    private const string EmptyInputErrorMessage = "Error: Input should not be empty";
    private const string InvalidInputErrorMessage =
        "Error: The input is invalid, it should include only numbers (and letters A to F in case of hexadecimal format, with prefix '0x')";

    private readonly CoreMaskInputValidator _inputValidator = new();

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("10")]
    [InlineData("01")]
    [InlineData("0x0")]
    [InlineData("0x1")]
    [InlineData("0x10")]
    [InlineData("0x01")]
    [InlineData("0xA")]
    [InlineData("0XA")]
    public void ValidateInput_WhenInputIsValid_ShouldReturnNull(string input)
    {
        // Arrange

        // Act
        var result = _inputValidator.ValidateInput(input);

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ValidateInput_WhenInputIsEmpty_ShouldReturnErrorMessage(string? input)
    {
        // Arrange

        // Act
        var result = _inputValidator.ValidateInput(input);

        // Assert
        Assert.Equal(EmptyInputErrorMessage, result);
    }

    [Theory]
    [InlineData("1,0")]
    [InlineData("-1")]
    [InlineData(" 1")]
    [InlineData("1 ")]
    [InlineData("A")]
    [InlineData("0x")]
    public void ValidateInput_WhenInputIsInvalid_ShouldReturnErrorMessage(string input)
    {
        // Arrange

        // Act
        var result = _inputValidator.ValidateInput(input);

        // Assert
        Assert.Equal(InvalidInputErrorMessage, result);
    }
}