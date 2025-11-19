using PMart.DeveloperTools.CoreMask.Calculators;

namespace CoreMask.Sample;

public class UseCoreNumbersCalculatorSample
{
    private readonly ICoreNumbersCalculator _coreNumbersCalculator;

    public UseCoreNumbersCalculatorSample(ICoreNumbersCalculator coreNumbersCalculator)
    {
        _coreNumbersCalculator = coreNumbersCalculator;
    }

    public void Calculate()
    {
        const string input = "93";

        var result = _coreNumbersCalculator.CalculateCoreNumbers(input);
        
        Console.WriteLine($"Core numbers: {string.Join(", ", result.CoreNumbers)}"); 
        Console.WriteLine($"Core mask (decimal): {result.CoreMask!.GetCoreMaskAsDecimal()}");
        Console.WriteLine($"Core mask (hex): {result.CoreMask.GetCoreMaskAsHexadecimal()}");
        Console.WriteLine($"Core mask (binary): {result.CoreMask.GetCoreMaskAsBinary()}");
        
        // It will print:
        // Core numbers: 0, 2, 3, 4, 6
        // Core mask (decimal): 93
        // Core mask (hex): 5D
        // Core mask (binary): 01011101
    }
}