using PMart.CoreMask.Validators;

namespace PMart.CoreMask.Tests.Validators;

public class CoreNumbersInputValidatorTests
{
    private const string EmptyInputErrorMessage = "Error: Input should not be empty";
    private const string InvalidInputErrorMessage =
        "Error: The input is invalid, it should include only numbers, ',' or '-' (example: 1-3,6,8-10)";
    
    private readonly CoreNumbersInputValidator _inputValidator = new();

    [Theory]
    [InlineData("0")]
    [InlineData("0,1")]
    [InlineData("0,1,2")]
    [InlineData("0-2")]
    [InlineData("1-3,6,8-10")]
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
    [InlineData(",0")]
    [InlineData("0,")]
    [InlineData("0,,2")]
    [InlineData("0,-")]
    [InlineData("-2")]
    [InlineData("2-")]
    [InlineData("0--2")]
    [InlineData(" 1")]
    [InlineData("1 ")]
    [InlineData("A")]
    [InlineData("0,a")]
    public void ValidateInput_WhenInputIsInvalid_ShouldReturnErrorMessage(string input)
    {
        // Arrange

        // Act
        var result = _inputValidator.ValidateInput(input);

        // Assert
        Assert.Equal(InvalidInputErrorMessage, result);
    }
}