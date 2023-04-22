using FluentAssertions;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Zenon.Model.Primitives
{
    public class HashTest
    {
        public static IEnumerable<object[]> ValidHashData()
        {
            yield return new object[]
            {
                "b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9"
            };
            yield return new object[]
            {
                "6a50df02d365fe8034881d0bac17be58d5af4f5efec37c4b965a7ba05a557df0"
            };
            yield return new object[]
            {
                "9ef6873791c43a3f380671970c58672e9604c617b529fca282b07e36576f8743"
            };
            yield return new object[]
            {
                "29e36ceb9e12c8dd5c7f42b7fc7e0236fe3df2ac558bd60d1d27e329f75e1514"
            };
        }

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
        [MemberData(nameof(ValidHashData))]
        public void When_ParseHash_ExpectToEqualHash(string hashString)
        {
            // Execute
            var hash = Hash.Parse(hashString);

            // Validate
            hash.ToString().Should().BeEquivalentTo(hashString);
        }

        [Fact]
        public void When_Digest_ExpectToEqual()
        {
            // Setup
            var data = Encoding.UTF8.GetBytes("Hello World!");

            // Execute
            var hash = Hash.Digest(data);

            // Validate
            hash.ToString().Should().BeEquivalentTo("d0e47486bbf4c16acac26f8b653592973c1362909f90262877089f9c8a4536af");
            hash.ToShortString().Should().BeEquivalentTo("d0e474...4536af");
        }
    }
}