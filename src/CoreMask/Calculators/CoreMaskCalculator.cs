using PMart.CoreMask.Constants;
using PMart.CoreMask.Helpers;
using PMart.CoreMask.Models;
using PMart.CoreMask.Parsers;
using PMart.CoreMask.Validators;

namespace PMart.CoreMask.Calculators;

/// <summary>
/// The core mask calculator.
/// </summary>
/// <seealso cref="ICoreMaskCalculator"/>
internal class CoreMaskCalculator : ICoreMaskCalculator
{
    private const string ParsingErrorMessage = "Error: not able to parse the input (invalid numbers?)";
    private const string MaxNumberOfCoresErrorMessage = "Error: the number of cores is more than the maximum supported";
    private const string MaxCoreNumberErrorMessage = "Error: core number higher than the maximum supported";

    private readonly ICoreNumbersInputValidator _inputValidator;
    private readonly ICoreNumbersInputParser _inputParser;

    /// <summary>
    /// Initializes a new instance of the <see cref="CoreMaskCalculator"/> class.
    /// </summary>
    /// <param name="inputValidator">The input validator.</param>
    /// <param name="inputParser">The input parser.</param>
    public CoreMaskCalculator(ICoreNumbersInputValidator inputValidator,
        ICoreNumbersInputParser inputParser)
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

        var cores = _inputParser.ParseInputToCoreNumbers(input!);

        if (cores is null)
        {
            return new CalculatorResult(errorMessage: ParsingErrorMessage);
        }

        return CalculateCoreMask(cores);
    }

    /// <inheritdoc />
    public CalculatorResult CalculateCoreMask(IEnumerable<ushort> coreNumbers)
    {
        var cores = coreNumbers.Distinct().OrderBy(c => c).ToList();

        if (cores.Count > CoreMaskConstants.MaxNumberOfCores)
        {
            return new CalculatorResult(errorMessage:
                $"{MaxNumberOfCoresErrorMessage} (number of cores: {cores.Count}, maximum: {CoreMaskConstants.MaxNumberOfCores})");
        }

        if (cores.LastOrDefault() > CoreMaskConstants.MaxCoreNumber)
        {
            return new CalculatorResult(errorMessage:
                $"{MaxCoreNumberErrorMessage} (maximum core number: {CoreMaskConstants.MaxCoreNumber})");
        }

        var coreMask = CoreMaskHelper.BuildCoreMask(cores);

        return new CalculatorResult(coreMask, cores);
    }
}