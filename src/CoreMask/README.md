# PMart.DeveloperTools.CoreMask

This library includes calculators related with [core masking](#core-mask-explained).

# Installation

Add the package to your project:
```bash
dotnet add package PMart.DeveloperTools.CoreMask
```

# Usage

Add the tools to the dependency injection container:

 ```c#
// Program.cs

using PMart.CoreMask.DependencyInjection;

// ...

services.AddCoreMaskTools();

// ...
```

Inject the calculator you need by dependency injection and use it. Check this [sample](../../samples/CoreMask.Sample/UseCoreMaskCalculatorSample.cs), for instance:

 ```c#
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
```

For a ready console app that uses the calculators, check this [app](../../src/CoreMask.App).

# Core Mask Explained

A core mask, or affinity mask, is used to define the set of CPU cores to assign a thread, process or application, to improve performance, for instance.
Generally, this mask is a bitmask, where each bit represents a CPU core:
- Each bit correspond to a CPU core number, with the least significant bit corresponding to core `0`.
- If a bit is set to 1, the corresponding core is to be used.

Example: a bitmask like `1010`, means the cores `1` and `3` are selected
(the rightmost bit corresponds to core `0` and is disabled, the next bit to the left, corresponding to core `1`, is enabled and the next one, corresponding to core `2` is disabled, and so on).

The bitmask `1010`, in its hexadecimal representation is `0xA` and in its decimal representation is `10`.

# Core Mask Calculators

## Core Mask Calculator

The `ICoreMaskCalculator` calculates the core mask from a collection of core numbers.

It has 2 methods:

 ```c#
public CalculatorResult CalculateCoreMask(string? input);
 
public CalculatorResult CalculateCoreMask(IEnumerable<ushort> coreNumbers);
```

Both methods do the same, but with different input types.

In the method `CalculateCoreMask(string? input)`, the input is a `string` with the core numbers, separated by `,` or by `-` to select a range of cores.
For example, the input `"0,2-4,6"` will result in a core mask for the cores 0, 2, 3, 4 and 6 (number `93` in its decimal representation).
The input should not have any other characters than numbers, comma or hyphens.
The method uses a validator and a parser and in case of validation or parsing error the result will include an error message.

The method `CalculateCoreMask(IEnumerable<ushort> coreNumbers)` is more straightforward, you provide directly the collection of the core numbers, in a form of an `IEnumerable<ushort>`.

## Core Numbers Calculator

The `ICoreNumbersCalculator` does the opposite of the `ICoreMaskCalculator`, calculates the core numbers from a core mask.

It has 2 methods:

 ```c#
public CalculatorResult CalculateCoreNumbers(string? input);

public CalculatorResult CalculateCoreNumbers(BigInteger coreMaskNumber);
```

Both methods do the same, but with different input types.

In the method `CalculateCoreNumbers(string? input)`, the input is a `string` with the core mask in its decimal representation.
For example, the input `"93"` will result in the core numbers 0, 2, 3, 4 and 6. The input should not have any other characters than numbers.
The method uses a validator and a parser and in case of validation or parsing error the result will include an error message.

The method `CalculateCoreNumbers(BigInteger coreMaskNumber)` is more straightforward, you provide directly the number, in a form of a `BigInteger`.

## CalculatorResult

Both calculators return the same result type: `CalculatorResult`.

This result have the following properties:
- `CoreMask`: the `CoreMask` object, with the core mask number.
- `CoreNumbers`: the list of cores included in the core mask.
- `ErrorMessage`: the error message in case the result is failed (otherwise it is `null`).
- `IsSuccess`: `bool` value indicating whether this is a successful result.

### CoreMask

The `CoreMask` object holds the core mask number, in the form of a `BigInteger`.

It also has helper methods to convert the number to decimal, hexadecimal or binary representation.

> The core mask number uses a `BigInteger` to support high core numbers. For instance, using a `ulong` (64 bits) would only support core numbers between 0 and 63.