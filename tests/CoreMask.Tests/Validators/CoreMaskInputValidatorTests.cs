using PMart.DeveloperTools.CoreMask.Validators;

namespace CoreMask.Tests.Validators;

public class CoreMaskInputValidatorTests
{
    private const string EmptyInputErrorMessage = "Error: Input should not be empty";
    private const string InvalidInputErrorMessage = "Error: The input is invalid, it should include only numbers";
    
    private readonly CoreMaskInputValidator _inputValidator = new();

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("10")]
    [InlineData("01")]
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
    public void ValidateInput_WhenInputIsInvalid_ShouldReturnErrorMessage(string input)
    {
        // Arrange

        // Act
        var result = _inputValidator.ValidateInput(input);

        // Assert
        Assert.Equal(InvalidInputErrorMessage, result);
    }
}