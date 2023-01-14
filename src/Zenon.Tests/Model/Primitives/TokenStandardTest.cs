using FluentAssertions;
using System;
using Xunit;
using Zenon.Utils;

namespace Zenon.Model.Primitives
{
    public class TokenStandardTest
    {
        [Theory]
        [InlineData("znn", "zts1znnxxxxxxxxxxxxx9z4ulx")]
        [InlineData("qsr", "zts1qsrxxxxxxxxxxxxxmrhjll")]
        public void When_BySymbol_ExpectSuccess(string symbol, string expectedZts)
        {
            // Execute
            var zts = TokenStandard.BySymbol(symbol);

            // Validate
            zts.ToString().Should().BeEquivalentTo(expectedZts);
        }

        [Theory]
        [InlineData("btc")]
        [InlineData("eth")]
        public void When_BySymbol_ExpectFailure(string symbol)
        {
            // Setup
            var expectedMessage = "TokenStandard.BySymbol supports only znn/qsr";

            // Execute
            var action = () => TokenStandard.BySymbol(symbol);

            // Validate
            action.Should().ThrowExactly<ArgumentException>().WithMessage(expectedMessage);
        }

        [Theory]
        [InlineData("000259a359013c81203d", "zts1qqp9ng6eqy7gzgpak0wya9")]
        public void When_FromBytes_ExpectToEqual(string hexString, string expectedZts)
        {
            // Execute
            var zts = TokenStandard.FromBytes(BytesUtils.FromHexString(hexString));

            // Validate
            zts.ToString().Should().BeEquivalentTo(expectedZts);
        }

        [Theory]
        [InlineData("03030303030303030303030303030303", "Invalid ZTS size. Expected 10 but got 16")]
        public void When_FromBytes_ExpectFailure(string hexString, string expectedMessage)
        {
            // Execute
            var action = () => TokenStandard.FromBytes(BytesUtils.FromHexString(hexString));

            // Validate
            action.Should().ThrowExactly<ArgumentException>().WithMessage(expectedMessage);
        }

        [Theory]
        [InlineData("zts1q803c0nd5cfj2sm4fv0yga")]
        [InlineData("zts183cs3vh0txu6sqc2m03rec")]
        [InlineData("zts1s5l6z3aseq6gce8xf8nsv9")]
        public void When_Parse_ExpectSuccess(string value)
        {
            // Execute
            var zts = TokenStandard.Parse(value);

            // Validate
            zts.ToString().Should().BeEquivalentTo(value);
        }

        [Theory]
        [InlineData("bc1rrrrrrrrrrrrrrrrz9dfqq", "Invalid ZTS prefix. Expected \"zts\" but got \"bc\"")]
        public void When_Parse_ExpectFailure(string value, string expectedMessage)
        {
            // Execute
            var action = () => TokenStandard.Parse(value);

            // Validate
            action.Should().ThrowExactly<ArgumentException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void EqualsReference()
        {
            TokenStandard.ZnnZts.Should().BeSameAs(TokenStandard.ZnnZts);
        }

        [Fact]
        public void EqualsOther()
        {
            TokenStandard.ZnnZts.Should().BeEquivalentTo(TokenStandard.Parse(TokenStandard.ZnnTokenStandard));
        }

        [Fact]
        public void NotEqualsNull()
        {
            TokenStandard.ZnnZts.Should().NotBeNull();
        }

        [Fact]
        public void NotEqualsOther()
        {
            TokenStandard.ZnnZts.Should().NotBeEquivalentTo(TokenStandard.QsrZts);
        }
    }
}