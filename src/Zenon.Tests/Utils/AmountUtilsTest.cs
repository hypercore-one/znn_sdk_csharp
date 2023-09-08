using FluentAssertions;
using System.Numerics;
using Xunit;

namespace Zenon.Utils
{
    public class AmountUtilsTest
    {
        [Theory]
        [InlineData("", 0, "0")]
        [InlineData("1", 0, "1")]
        [InlineData("1", 1, "10")]
        [InlineData("1", 2, "100")]
        [InlineData("1", 3, "1000")]
        [InlineData("1", 4, "10000")]
        [InlineData("1", 5, "100000")]
        [InlineData("1", 6, "1000000")]
        [InlineData("1", 7, "10000000")]
        [InlineData("1.", 0, "1")]
        [InlineData("1.00", 0, "1")]
        [InlineData("1.00", 1, "10")]
        [InlineData("1.00", 2, "100")]
        [InlineData(".1", 1, "1")]
        [InlineData(".00000001", 8, "1")]
        [InlineData(".11111111", 0, "0")]
        [InlineData(".11111111", 1, "1")]
        [InlineData(".11111111", 2, "11")]
        [InlineData(".11111111", 3, "111")]
        [InlineData(".11111111", 4, "1111")]
        [InlineData(".11111111", 5, "11111")]
        [InlineData(".11111111", 6, "111111")]
        [InlineData(".11111111", 7, "1111111")]
        [InlineData(".11111111", 8, "11111111")]
        public void When_ExtractDecimals_ExpectToEqual(string amount, int decimals, string expectedResult)
        {
            // Execute
            var result = AmountUtils.ExtractDecimals(amount, decimals);

            // Validate
            result.ToString().Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData("1", 0, "1")]
        [InlineData("1", 1, ".1")]
        [InlineData("1", 2, ".01")]
        [InlineData("1", 3, ".001")]
        [InlineData("1", 4, ".0001")]
        [InlineData("1", 5, ".00001")]
        [InlineData("1", 6, ".000001")]
        [InlineData("1", 7, ".0000001")]
        [InlineData("1", 8, ".00000001")]
        [InlineData("10000000", 0, "10000000")]
        [InlineData("10000000", 1, "1000000")]
        [InlineData("10000000", 2, "100000")]
        [InlineData("10000000", 3, "10000")]
        [InlineData("10000000", 4, "1000")]
        [InlineData("10000000", 5, "100")]
        [InlineData("10000000", 6, "10")]
        [InlineData("10000000", 7, "1")]
        [InlineData("10000000", 8, ".1")]
        [InlineData("11111111", 0, "11111111")]
        [InlineData("11111111", 1, "1111111.1")]
        [InlineData("11111111", 2, "111111.11")]
        [InlineData("11111111", 3, "11111.111")]
        [InlineData("11111111", 4, "1111.1111")]
        [InlineData("11111111", 5, "111.11111")]
        [InlineData("11111111", 6, "11.111111")]
        [InlineData("11111111", 7, "1.1111111")]
        [InlineData("11111111", 8, ".11111111")]
        public void When_AddDecimals_ExpectToEqual(string amount, int decimals, string expectedResult)
        {
            // Setup
            var value = BigInteger.Parse(amount);

            // Execute
            var result = AmountUtils.AddDecimals(value, decimals);

            // Validate
            result.ToString().Should().BeEquivalentTo(expectedResult);
        }
    }
}
