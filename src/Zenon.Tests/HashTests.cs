using FluentAssertions;
using Xunit;
using Zenon.Model.Primitives;
using Zenon.Tests.TestData;

namespace Zenon.Tests
{
    public class HashTests
    {
        [Fact]
        public void When_ParseEmpty_ExpectToEqualEmpty()
        {
            // Setup
            var emptyHash = "0000000000000000000000000000000000000000000000000000000000000000";

            // Execute
            var hash = Hash.Parse(emptyHash);

            // Validate
            hash.ToString().Should().BeEquivalentTo(emptyHash);
        }

        [Theory]
        [ClassData(typeof(HashTestData))]
        public void When_ParseHash_ExpectToEqualHash(string hashString)
        {
            // Execute
            var hash = Hash.Parse(hashString);

            // Validate
            hash.ToString().Should().BeEquivalentTo(hashString);
        }
    }
}