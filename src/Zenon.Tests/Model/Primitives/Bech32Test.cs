using FluentAssertions;
using System;
using Xunit;

namespace Zenon.Model.Primitives
{
    public class Bech32Test
    {
        [Theory]
        [InlineData("bc1pw508d6qejxtdg4y5r3zarvary0c5xw7kw508d6qejxtdg4y5r3zarvary0c5xw7k7grplx", "bc", "010e140f070d1a001912060b0d081504140311021d030c1d03040f1814060e1e160e140f070d1a001912060b0d081504140311021d030c1d03040f1814060e1e16")]
        [InlineData("customhrp!11111q123jhxapqv3shgcgkxpuhe", "customhrp!11111q", "0A111217061D01000C111017081808")]
        [InlineData("z1qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqsggv2f", "z", "0000000000000000000000000000000000000000000000000000000000000000")]
        [InlineData("z1qxemdeddedxplasmaxxxxxxxxxxxxxxxxsctrp", "z", "0006191b0d190d0d190d06011f1d101b1d060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg", "z", "0006191b0d190d0d190d0601041f1f1d03060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxt0kenxxxxxxxxxxxxxxxxh9amk0", "z", "0006191b0d190d0d190d060b0f16191306060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r", "z", "0006191b0d190d0d190d061019130b0413191f06060606060606060606060606")]
        [InlineData("z1qxemdeddedxswapxxxxxxxxxxxxxxxxxxl4yww", "z", "0006191b0d190d0d190d06100e1d010606060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxstakexxxxxxxxxxxxxxxxjv8v62", "z", "0006191b0d190d0d190d06100b1d161906060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxsp0rkxxxxxxxxxxxxxxxx956u48", "z", "0006191b0d190d0d190d0610010f031606060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxaccelerat0rxxxxxxxxxxp4tk22", "z", "0006191b0d190d0d190d061d1818191f19031d0b0f0306060606060606060606")]
        [InlineData("z1qzlytaqdahg5t02nz5096frflfv7dm3y7yxmg7", "z", "00021f040b1d000d1d1708140b0f0a1302140f051a0903091f090c1e0d1b1104")]
        public void When_DecodeBech32_ExpectValid(string encoded, string hrp, string decoded)
        {
            // Setup
            var expectedDecodedBytes = Convert.FromHexString(decoded);

            // Execute
            var bech32 = Bech32Codec.Decode(encoded, 40);

            // Validate
            bech32.Hrp.Should().Be(hrp);
            bech32.Data.Should().BeEquivalentTo(expectedDecodedBytes);
        }

        [Theory]
        [InlineData("bc1pw508d6qejxtdg4y5r3zarvary0c5xw7kw508d6qejxtdg4y5r3zarvary0c5xw7k7grplx", "bc", "010e140f070d1a001912060b0d081504140311021d030c1d03040f1814060e1e160e140f070d1a001912060b0d081504140311021d030c1d03040f1814060e1e16")]
        [InlineData("customhrp!11111q123jhxapqv3shgcgkxpuhe", "customhrp!11111q", "0A111217061D01000C111017081808")]
        [InlineData("z1qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqsggv2f", "z", "0000000000000000000000000000000000000000000000000000000000000000")]
        [InlineData("z1qxemdeddedxplasmaxxxxxxxxxxxxxxxxsctrp", "z", "0006191b0d190d0d190d06011f1d101b1d060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg", "z", "0006191b0d190d0d190d0601041f1f1d03060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxt0kenxxxxxxxxxxxxxxxxh9amk0", "z", "0006191b0d190d0d190d060b0f16191306060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r", "z", "0006191b0d190d0d190d061019130b0413191f06060606060606060606060606")]
        [InlineData("z1qxemdeddedxswapxxxxxxxxxxxxxxxxxxl4yww", "z", "0006191b0d190d0d190d06100e1d010606060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxstakexxxxxxxxxxxxxxxxjv8v62", "z", "0006191b0d190d0d190d06100b1d161906060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxsp0rkxxxxxxxxxxxxxxxx956u48", "z", "0006191b0d190d0d190d0610010f031606060606060606060606060606060606")]
        [InlineData("z1qxemdeddedxaccelerat0rxxxxxxxxxxp4tk22", "z", "0006191b0d190d0d190d061d1818191f19031d0b0f0306060606060606060606")]
        [InlineData("z1qzlytaqdahg5t02nz5096frflfv7dm3y7yxmg7", "z", "00021f040b1d000d1d1708140b0f0a1302140f051a0903091f090c1e0d1b1104")]
        public void When_EncodeBech32_ExpectValid(string encoded, string hrp, string decoded)
        {
            // Setup
            var bech32 = new Bech32(hrp, Convert.FromHexString(decoded));

            // Execute
            var encodedString = Bech32Codec.Encode(bech32, 40);

            // Validate
            encodedString.Should().Be(encoded);
        }
    }
}
