using FluentAssertions;
using System;
using Xunit;

namespace Zenon.Abi
{
    public class AbiTest
    {
        [Theory]
        [ClassData(typeof(AbiTypeEncodeValidTestData))]
        public void When_EncodeAbiType_ExpectToEqual(AbiType type, object value, byte[] expectedResult)
        {
            // Execute
            var result = type.Encode(value);

            // Validate
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [ClassData(typeof(AbiTypeEncodeInvalidTestData))]
        public void When_EncodeAbiType_ExpectToFail(AbiType type, object value)
        {
            // Execute
            var action = new Action(() => type.Encode(value));

            // Validate
            action.Should().Throw<Exception>();
        }

        [Theory]
        [ClassData(typeof(AbiTypeDecodeValidTestData))]
        public void When_DecodeAbiType_ExpectToEqual(AbiType type, byte[] value, object expectedResult)
        {
            // Execute
            var result = type.Decode(value, 0);

            // Validate
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
