namespace PMart.CoreMask.Models;

/// <summary>
/// The result of the calculator.
/// </summary>
public record CalculatorResult
{
    /// <summary>
    /// The core mask (if succeeded).
    /// </summary>
    public CoreMask? CoreMask { get; }

    /// <summary>
    /// The core numbers included in the mask.
    /// </summary>
    public IEnumerable<ushort> CoreNumbers { get; } = [];

    /// <summary>
    /// The error message (in case of error).
    /// </summary>
    public string? ErrorMessage { get; }
    
    /// <summary>
    /// Value indicating whether this is a successful result.
    /// </summary>
    public bool IsSuccess => CoreMask is not null && ErrorMessage is null;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorResult"/> class.
    /// </summary>
    /// <param name="coreMask">The core mask.</param>
    /// <param name="coreNumbers">The core numbers included in the mask.</param>
    public CalculatorResult(CoreMask coreMask, IEnumerable<ushort> coreNumbers)
    {
        CoreMask = coreMask;
        CoreNumbers = coreNumbers;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorResult"/> class with error.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    public CalculatorResult(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}