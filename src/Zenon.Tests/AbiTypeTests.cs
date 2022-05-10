using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Xunit;
using Zenon.Utils;

namespace Zenon.Tests
{
    internal class AbiTypeTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new BigInteger(179032.6541), 
                new byte[] { 2, 187, 88 }
            };
            yield return new object[]
            {
                BigInteger.Parse("934157136952"),
                new byte[] { 217, 128, 26, 180, 56 }
            };
            yield return new object[]
            {
                BigInteger.Parse("6315489358112"), 
                new byte[] { 5, 190, 112, 127, 241, 32 }
            };
            yield return new object[]
            {
                BigInteger.Parse("91389681247993671255432112000000"), 
                new byte[] { 4, 129, 127, 252, 132, 7, 160, 14, 178, 250, 248, 218, 76, 0 }
            };
            yield return new object[]
            {
                BigInteger.Parse("5BE707FF120", NumberStyles.HexNumber), 
                new byte[] { 5, 190, 112, 127, 241, 32 }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class AbiTypeTests
    {
        [Theory]
        [ClassData(typeof(AbiTypeTestData))]
        public void When_EncodeBigInt_ExpectToEqual(BigInteger expectedValue, byte[] expectedBytes)
        {
            // Execute
            var bytes = BytesUtils.EncodeBigInt(expectedValue);
            var value = BytesUtils.DecodeBigInt(bytes);

            // Valudate
            value.Should().BeEquivalentTo(expectedValue);
            bytes.Should().BeEquivalentTo(expectedBytes);
        }
    }
}
