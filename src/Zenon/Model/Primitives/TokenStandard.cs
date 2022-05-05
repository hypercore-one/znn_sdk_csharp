using System;
using System.Linq;

namespace Zenon.Model.Primitives
{
    public class TokenStandard
    {
        public const string ZnnTokenStandard = "zts1znnxxxxxxxxxxxxx9z4ulx";
        public const string QsrTokenStandard = "zts1qsrxxxxxxxxxxxxxmrhjll";
        public const string EmptyTokenStandard = "zts1qqqqqqqqqqqqqqqqtq587y";

        public static readonly TokenStandard ZnnZts = Parse(ZnnTokenStandard);
        public static readonly TokenStandard QsrZts = Parse(QsrTokenStandard);
        public static readonly TokenStandard EmptyZts = Parse(EmptyTokenStandard);

        public const string Prefix = "zts";
        public const int CoreSize = 10;

        public static bool operator ==(TokenStandard obj1, TokenStandard obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            if (ReferenceEquals(obj1, null))
                return false;
            if (ReferenceEquals(obj2, null))
                return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(TokenStandard obj1, TokenStandard obj2) => !(obj1 == obj2);

        public static TokenStandard Parse(string tokenStandard)
        {
            var bech32 = Bech32Codec.Decode(tokenStandard);
            var hrp = bech32.Hrp;
            var core = Bech32Codec.ConvertBech32Bits(bech32.Data, 5, 8, false);
            return new TokenStandard(hrp, core);
        }

        public static TokenStandard FromBytes(byte[] bytes)
        {
            return new TokenStandard(Prefix, bytes);
        }


        public static TokenStandard BySymbol(string symbol)
        {
            if (symbol.CompareTo("znn") == 0 || symbol.CompareTo("ZNN") == 0)
            {
                return ZnnZts;
            }
            else if (symbol.CompareTo("qsr") == 0 || symbol.CompareTo("QSR") == 0)
            {
                return QsrZts;
            }
            else
            {
                throw new ArgumentException("TokenStandard.BySymbol supports only znn/qsr");
            }
        }

        private TokenStandard(string hrp, byte[] bytes)
        {
            if (hrp != Prefix)
            {
                throw new ArgumentException($"invalid ZTS prefix. Expected \"{Prefix}\" but got \"{hrp}\"");
            }
            if (bytes.Length != CoreSize)
            {
                throw new ArgumentException($"invalid ZTS size. Expected {CoreSize} but got {bytes.Length}");
            }

            Hrp = hrp;
            Bytes = bytes;
        }

        public string Hrp { get; }
        public byte[] Bytes { get; }

        public override string ToString()
        {
            var bech32 = new Bech32(Hrp, Bech32Codec.ConvertBech32Bits(Bytes, 8, 5, true));
            return Bech32Codec.Encode(bech32);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj) => Equals(obj as TokenStandard);

        public bool Equals(TokenStandard other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Hrp == other.Hrp &&
                Bytes.SequenceEqual(other.Bytes);
        }
    }
}
