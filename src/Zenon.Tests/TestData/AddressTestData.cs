using System;
using System.Collections;
using System.Collections.Generic;

namespace Zenon.Tests.TestData
{
    internal class AddressTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                "z1qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqsggv2f",
                "z1qqqqq...sggv2f",
                "z",
                Convert.FromHexString("0000000000000000000000000000000000000000"),
                false
            };
            yield return new object[]
            {
                "z1qxemdeddedxplasmaxxxxxxxxxxxxxxxxsctrp",
                "z1qxemd...xsctrp",
                "z",
                Convert.FromHexString("01b3b6e5adcb4c1ff61be98c6318c6318c6318c6"),
                true
            };
            yield return new object[]
            {
                "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                "z1qxemd...sy3fmg",
                "z",
                Convert.FromHexString("01b3b6e5adcb4c127ffd198c6318c6318c6318c6"),
                true
            };
            yield return new object[]
            {
                "z1qxemdeddedxt0kenxxxxxxxxxxxxxxxxh9amk0",
                "z1qxemd...h9amk0",
                "z",
                Convert.FromHexString("01b3b6e5adcb4cb7db33318c6318c6318c6318c6"),
                true
            };
            yield return new object[]
            {
                "z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r",
                "z1qxemd...wy0r2r",
                "z",
                Convert.FromHexString("01b3b6e5adcb4d0ccd649e7e6318c6318c6318c6"),
                true
            };
            yield return new object[]
            {
                "z1qxemdeddedxswapxxxxxxxxxxxxxxxxxxl4yww",
                "z1qxemd...xl4yww",
                "z",
                Convert.FromHexString("01b3b6e5adcb4d077426318c6318c6318c6318c6"),
                true
            };
            yield return new object[]
            {
                "z1qxemdeddedxstakexxxxxxxxxxxxxxxxjv8v62",
                "z1qxemd...jv8v62",
                "z",
                Convert.FromHexString("01b3b6e5adcb4d05f6d9318c6318c6318c6318c6"),
                true
            };
            yield return new object[]
            {
                "z1qxemdeddedxsp0rkxxxxxxxxxxxxxxxx956u48",
                "z1qxemd...956u48",
                "z",
                Convert.FromHexString("01b3b6e5adcb4d00bc76318c6318c6318c6318c6"),
                false
            };
            yield return new object[]
            {
                "z1qxemdeddedxaccelerat0rxxxxxxxxxxp4tk22",
                "z1qxemd...p4tk22",
                "z",
                Convert.FromHexString("01b3b6e5adcb4ddc633fc8fab78cc6318c6318c6"),
                true
            };
            yield return new object[]
            {
                "z1qzlytaqdahg5t02nz5096frflfv7dm3y7yxmg7",
                "z1qzlyt...7yxmg7",
                "z",
                Convert.FromHexString("00be45f40dedd145bd53151e5d2469fa59e6ee24"),
                false
            };
            yield return new object[]
            {
                "z1qrs43le3a5pdrn8jk0jwl8xu6ltxjy2g8mz2ls",
                "z1qrs43...8mz2ls",
                "z",
                Convert.FromHexString("00e158ff31ed02d1ccf2b3e4ef9cdcd7d6691148"),
                false
            };
            yield return new object[]
            {
                "z1qzzs6aju4vz4d8ndqch8kw8v8v97tcs9vhujvx",
                "z1qzzs6...vhujvx",
                "z",
                Convert.FromHexString("00850d765cab05569e6d062e7b38ec3b0be5e205"),
                false
            };
            yield return new object[]
            {
                "z1qr6f4pmyycu44emt9t8cshkvlvqvdj7s22l6tw",
                "z1qr6f4...22l6tw",
                "z",
                Convert.FromHexString("00f49a876426395ae76b2acf885eccfb00c6cbd0"),
                false
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
