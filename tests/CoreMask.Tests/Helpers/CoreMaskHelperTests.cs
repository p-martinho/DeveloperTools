using System.Numerics;
using PMart.DeveloperTools.CoreMask.Helpers;

namespace CoreMask.Tests.Helpers;

public class CoreMaskHelperTests
{
    [Theory]
    [MemberData(nameof(CoreMaskTheoryData))]
    public void BuildCoreMask_ShouldSucceed(ushort[] coreNumbers, BigInteger expectedCoreMask)
    {
        // Arrange

        // Act
        var result = CoreMaskHelper.BuildCoreMask(coreNumbers);

        // Assert
        Assert.Equal(expectedCoreMask, result.CoreMaskNumber);
    }
    
    [Theory]
    [MemberData(nameof(CoreNumbersTheoryData))]
    public void GetCoreNumbers_ShouldSucceed(BigInteger coreMask, ushort[] expectedCoreNumbers)
    {
        // Arrange

        // Act
        var result = CoreMaskHelper.GetCoreNumbers(coreMask);

        // Assert
        Assert.Equal(expectedCoreNumbers.Length, result.Count);
        for (var i = 0; i < expectedCoreNumbers.Length; i++)
        {
            Assert.Equal(expectedCoreNumbers[i], result[i]);
        }
    }
    
    public static IEnumerable<TheoryDataRow<ushort[], BigInteger>> CoreMaskTheoryData =>
    [
        new([0], 1), // Bin: 1
        new([0, 1], 3), // Bin: 11
        new([1, 2, 3], 14), // Bin: 1110
        new([0, 2, 4, 5], 53), // Bin: 110101
        new([255], BigInteger.One << 255), // 256 bits, first bit enabled
        new([], 0),
        new([0, 0], 1),
        new([5, 2, 4, 5, 0, 0], 53) // unordered, and with duplicates
    ];
    
    public static IEnumerable<TheoryDataRow<BigInteger, ushort[]>> CoreNumbersTheoryData =>
    [
        new(1, [0]), // Bin: 1
        new(3, [0, 1]), // Bin: 11
        new(14, [1, 2, 3]), // Bin: 1110
        new(53, [0, 2, 4, 5]), // Bin: 110101
        new(BigInteger.One << 255, [255]), // 256 bits, first bit enabled
        new(-1, []),
        new(0, [])
    ];
}