using System;
using System.Linq;

namespace Zenon.Model.Primitives
{
    public class Address : IComparable<Address>, IEquatable<Address>
    {
        public const string Prefix = "z";
        public const int AddressLength = 40;
        public const int UserByte = 0;
        public const int ContractByte = 1;
        public const int CoreSize = 20;

        public static readonly Address EmptyAddress =
            Parse("z1qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqsggv2f");
        public static readonly Address PlasmaAddress =
            Parse("z1qxemdeddedxplasmaxxxxxxxxxxxxxxxxsctrp");
        public static readonly Address PillarAddress =
            Parse("z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg");
        public static readonly Address TokenAddress =
            Parse("z1qxemdeddedxt0kenxxxxxxxxxxxxxxxxh9amk0");
        public static readonly Address SentinelAddress =
            Parse("z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r");
        public static readonly Address SwapAddress =
            Parse("z1qxemdeddedxswapxxxxxxxxxxxxxxxxxxl4yww");
        public static readonly Address StakeAddress =
            Parse("z1qxemdeddedxstakexxxxxxxxxxxxxxxxjv8v62");
        public static readonly Address SporkAddress =
            Parse("z1qxemdeddedxsp0rkxxxxxxxxxxxxxxxx956u48");
        public static readonly Address AcceleratorAddress =
            Parse("z1qxemdeddedxaccelerat0rxxxxxxxxxxp4tk22");

        public static readonly Address[] EmbeddedContractAddresses = new Address[]
        {
            PlasmaAddress,
            PillarAddress,
            TokenAddress,
            SentinelAddress,
            SwapAddress,
            StakeAddress,
            AcceleratorAddress
        };

        public static bool operator ==(Address obj1, Address obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            if (ReferenceEquals(obj1, null))
                return false;
            if (ReferenceEquals(obj2, null))
                return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Address obj1, Address obj2) => !(obj1 == obj2);

        public static Address Parse(string address)
        {
            var bech32 = Bech32Codec.Decode(address, AddressLength);
            var core = Bech32Codec.ConvertBech32Bits(bech32.Data, 5, 8, false);
            return new Address(bech32.Hrp, core);
        }

        public static bool IsValid(string address)
        {
            try
            {
                var a = Parse(address);
                return a.ToString() == address;
            }
            catch
            {
                return false;
            }
        }

        public static Address FromPublicKey(byte[] publicKey)
        {
            using (var shaAlg = SHA3.Net.Sha3.Sha3256())
            {
                var hash = shaAlg.ComputeHash(publicKey);

                var core = new byte[CoreSize];
                core[0] = UserByte;
                Buffer.BlockCopy(hash, 0, core, 1, 19);

                return new Address(Prefix, core);
            }
        }

        public Address(string hrp, byte[] core)
        {
            Hrp = hrp;
            Bytes = core;
        }

        public string Hrp { get; }
        public byte[] Bytes { get; }

        public override string ToString()
        {
            var bech32 = new Bech32(Hrp, Bech32Codec.ConvertBech32Bits(Bytes, 8, 5, true));
            var addressStr = Bech32Codec.Encode(bech32, AddressLength);
            return addressStr;
        }

        public string ToShortString()
        {
            var longString = ToString();
            return longString.Substring(0, 7) + "..." + longString.Substring(longString.Length - 6);
        }


        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool IsEmbedded
        {
            get
            {
                return EmbeddedContractAddresses.Contains(this);
            }
        }

        public int CompareTo(Address other)
        {
            return ToString().CompareTo(other.ToString());
        }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Hrp == other.Hrp &&
                Bytes.SequenceEqual(other.Bytes);
        }

        public override bool Equals(object obj) => Equals(obj as Address);
    }
}
