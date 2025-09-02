using System.Numerics;

namespace PMart.CoreMask.Models;

/// <summary>
/// The core mask.
/// </summary>
public record CoreMask(BigInteger CoreMaskNumber)
{
    /// <summary>
    /// The core mask in decimal format.
    /// </summary>
    public string CoreMaskAsDecimal => CoreMaskNumber.ToString();
    
    /// <summary>
    /// The core mask in hexadecimal format.
    /// </summary>
    public string CoreMaskAsHexadecimal => CoreMaskNumber.ToString("X");
    
    /// <summary>
    /// The core mask in binary format.
    /// </summary>
    public string CoreMaskAsBinary => CoreMaskNumber.ToString("B");
}