using FluentAssertions;
using Xunit;
using Zenon.Tests.TestData;

namespace Zenon.Tests
{
    public class AbiTests
    {
        [Theory]
        [ClassData(typeof(AbiFunctionTestData))]
        public void When_EncodeFunction_ExpectToEqual(Abi.Abi definition, string name, object[] args, byte[] expectedEncoded)
        {
            // Execute
            var encoded = definition.EncodeFunction(name, args);

            // Validate
            expectedEncoded.Should().BeEquivalentTo(encoded);
        }
    }
}
