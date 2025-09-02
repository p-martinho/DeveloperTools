using System.Numerics;
using PMart.CoreMask.Constants;
using PMart.CoreMask.Helpers;
using PMart.CoreMask.Models;
using PMart.CoreMask.Parsers;
using PMart.CoreMask.Validators;

namespace PMart.CoreMask.Calculators;

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
    public CalculatorResult CalculateCoreMask(string? input)
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
        
        if (coreMaskNumber <= 0)
        {
            return new CalculatorResult(errorMessage: InvalidNumberErrorMessage);
        }

        if (coreMaskNumber > BigInteger.One << CoreMaskConstants.MaxCoreNumber)
        {
            return new CalculatorResult(errorMessage: MaxCoreNumberErrorMessage);
        }

        var cores = CoreMaskHelper.GetCoreNumbers(coreMaskNumber.Value);

        return new CalculatorResult(new Models.CoreMask(coreMaskNumber.Value), cores);
    }
}