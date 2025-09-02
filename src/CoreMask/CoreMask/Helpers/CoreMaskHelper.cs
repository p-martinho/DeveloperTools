using System.Numerics;

namespace PMart.CoreMask.Helpers;

/// <summary>
/// The core mask helper.
/// </summary>
internal static class CoreMaskHelper
{
    /// <summary>
    /// Build the core mask from the core numbers provided.
    /// </summary>
    /// <param name="cores">The core numbers to add to the mask.</param>
    /// <returns>The core mask.</returns>
    public static Models.CoreMask BuildCoreMask(IEnumerable<ushort> cores)
    {
        var coreMaskNumber = BigInteger.Zero;

        foreach (var core in cores)
        {
            var bitToSet = BigInteger.One << core;
    
            coreMaskNumber |= bitToSet;
        }

        return new Models.CoreMask(coreMaskNumber);
    }
    
    /// <summary>
    /// Gets the core numbers from the core mask provided.
    /// </summary>
    /// <param name="coreMaskNumber">The core mask number.</param>
    /// <returns>The collection with the core numbers.</returns>
    public static IList<ushort> GetCoreNumbers(BigInteger coreMaskNumber)
    {
        var coreNumbers = new List<ushort>();
        
        ushort coreIndex = 0;

        // Loop as long as the mask has bits set
        while (coreMaskNumber > 0)
        {
            // Check if the current least significant bit is set (i.e., is 1).
            // A bitwise AND with 1 will be 1 if the bit is set, and 0 otherwise.
            if ((coreMaskNumber & 1) == 1)
            {
                coreNumbers.Add(coreIndex);
            }
            
            // Right-shift the mask by one position.
            coreMaskNumber >>= 1;

            // Increment the counter to track the next core number.
            coreIndex++;
        }

        return coreNumbers;
    }
}