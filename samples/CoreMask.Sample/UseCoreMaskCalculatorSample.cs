using PMart.CoreMask.Calculators;

namespace CoreMask.Sample;

public class UseCoreMaskCalculatorSample
{
    private readonly ICoreMaskCalculator _coreMaskCalculator;

    public UseCoreMaskCalculatorSample(ICoreMaskCalculator coreMaskCalculator)
    {
        _coreMaskCalculator = coreMaskCalculator;
    }

    public void Calculate()
    {
        const string input = "0,2-4,6";

        var result = _coreMaskCalculator.CalculateCoreMask(input);
        
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