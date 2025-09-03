using Microsoft.Extensions.DependencyInjection;
using PMart.CoreMask.Calculators;
using PMart.CoreMask.DependencyInjection;
using PMart.CoreMask.Models;

try
{
    Console.WriteLine("Core Mask App");
    
    var serviceProvider = CreateServices();
    
    var coreMaskCalculator = serviceProvider.GetRequiredService<ICoreMaskCalculator>();
    var coreNumbersCalculator = serviceProvider.GetRequiredService<ICoreNumbersCalculator>();
    
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("Choose an operation:");
        Console.WriteLine("1. Calculate mask from core numbers");
        Console.WriteLine("2. Calculate core numbers from mask");
        Console.WriteLine("3. Exit");
        Console.Write("Enter your choice (1, 2 or 3): ");

        var choice = Console.ReadLine();
        Console.WriteLine();

        CalculatorResult result;

        switch (choice)
        {
            case "1":
                result = CalculateMaskFromCores(coreMaskCalculator);
                break;
            case "2":
                result = CalculateCoreNumbersFromMask(coreNumbersCalculator);
                break;
            case "3":
                return 0;
            default:
                Console.WriteLine("Invalid choice. Please enter 1, 2 or 3.");
                continue;
        }
        
        PrintResult(result);
    }
}
catch (Exception e)
{
    Console.WriteLine($"An unexpected error occured: {e.Message}");

    return -1;
}

static ServiceProvider CreateServices()
{
    var services = new ServiceCollection();
        
    services.AddCoreMaskTools();

    return services.BuildServiceProvider();
}

static CalculatorResult CalculateMaskFromCores(ICoreMaskCalculator calculator)
{
    Console.WriteLine("What cores to use?");

    var input = Console.ReadLine();
    
    return calculator.CalculateCoreMask(input);
}

static CalculatorResult CalculateCoreNumbersFromMask(ICoreNumbersCalculator calculator)
{
    Console.WriteLine("What is the core mask?");

    var input = Console.ReadLine();

    return calculator.CalculateCoreNumbers(input);
}

static void PrintResult(CalculatorResult result)
{
    if (!result.IsSuccess)
    {
        Console.WriteLine(result.ErrorMessage);
        return;
    }
        
    Console.WriteLine($"Core mask for {result.CoreNumbers.Count()} core(s): {string.Join(", ", result.CoreNumbers)}");
    Console.WriteLine($"Core mask (decimal): {result.CoreMask!.GetCoreMaskAsDecimal()}");
    Console.WriteLine($"Core mask (hex): {result.CoreMask.GetCoreMaskAsHexadecimal()}");
    Console.WriteLine($"Core mask (binary): {result.CoreMask.GetCoreMaskAsBinary()}");
}