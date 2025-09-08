using System.Numerics;
using PMart.DeveloperTools.CoreMask.Constants;
using PMart.DeveloperTools.CoreMask.Helpers;
using PMart.DeveloperTools.CoreMask.Models;
using PMart.DeveloperTools.CoreMask.Parsers;
using PMart.DeveloperTools.CoreMask.Validators;

namespace PMart.DeveloperTools.CoreMask.Calculators;

/// <summary>
/// The core numbers calculator.
/// </summary>
/// <seealso cref="ICoreNumbersCalculator"/>
internal class CoreNumbersCalculator : ICoreNumbersCalculator
{
    private const string ParsingErrorMessage = "Error: not able to parse the input (invalid number?)";
    private const string MaxCoreNumberErrorMessage = "Error: core number higher than the maximum supported";
    private const string InvalidNumberErrorMessage = "Error: the number is invalid (must be an integer higher than 0)";

    private readonly ICoreMaskInputValidator _inputValidator;
    private readonly ICoreMaskInputParser _inputParser;

    /// <summary>
    /// Initializes a new instance of the <see cref="CoreNumbersCalculator"/> class.
    /// </summary>
    /// <param name="inputValidator">The input validator.</param>
    /// <param name="inputParser">The input parser.</param>
    public CoreNumbersCalculator(ICoreMaskInputValidator inputValidator,
        ICoreMaskInputParser inputParser)
    {
        _inputValidator = inputValidator;
        _inputParser = inputParser;
    }

    /// <inheritdoc />
    public CalculatorResult CalculateCoreNumbers(string? input)
    {
        var inputError = _inputValidator.ValidateInput(input);

        if (inputError is not null)
        {
            return new CalculatorResult(errorMessage: inputError);
        }

        var coreMaskNumber = _inputParser.ParseInputToCoreMask(input!);

        if (coreMaskNumber is null)
        {
            return new CalculatorResult(errorMessage: ParsingErrorMessage);
        }
        
        return CalculateCoreNumbers(coreMaskNumber.Value);
    }

    /// <inheritdoc />
    public CalculatorResult CalculateCoreNumbers(BigInteger coreMaskNumber)
    {
        if (coreMaskNumber <= 0)
        {
            return new CalculatorResult(errorMessage: InvalidNumberErrorMessage);
        }

        if (coreMaskNumber > BigInteger.One << CoreMaskConstants.MaxCoreNumber)
        {
            return new CalculatorResult(errorMessage: MaxCoreNumberErrorMessage);
        }

        var cores = CoreMaskHelper.GetCoreNumbers(coreMaskNumber);

        return new CalculatorResult(new Models.CoreMask(coreMaskNumber), cores);
    }
}