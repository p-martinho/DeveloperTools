using System.Numerics;

namespace PMart.DeveloperTools.CoreMask.Models;

/// <summary>
/// The core mask.
/// </summary>
public record CoreMask(BigInteger CoreMaskNumber)
{
    /// <summary>
    /// Gets the core mask in decimal representation.
    /// </summary>
    public string GetCoreMaskAsDecimal() => CoreMaskNumber.ToString();
    
    /// <summary>
    /// Gets the core mask in hexadecimal representation.
    /// </summary>
    public string GetCoreMaskAsHexadecimal() => CoreMaskNumber.ToString("X");
    
    /// <summary>
    /// Gets the core mask in binary representation.
    /// </summary>
    public string GetCoreMaskAsBinary() => CoreMaskNumber.ToString("B");
}