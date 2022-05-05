using System;
using System.Linq;

namespace Zenon.Model.Primitives
{
    public class Hash
    {
        public const int Length = 32;

        public static Hash Empty = Parse("0000000000000000000000000000000000000000000000000000000000000000");

        public static bool operator ==(Hash obj1, Hash obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            if (ReferenceEquals(obj1, null))
                return false;
            if (ReferenceEquals(obj2, null))
                return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Hash obj1, Hash obj2) => !(obj1 == obj2);

        public static Hash FromBytes(byte[] byteArray)
        {
            if (byteArray.Length != Length)
            {
                throw new ArgumentException("Invalid hash length");
            }

            return new Hash(byteArray);
        }

        public static Hash Parse(string hashString)
        {
            if (hashString.Length != 2 * Length)
            {
                throw new ArgumentException("Invalid hash length");
            }

            return new Hash(Convert.FromHexString(hashString));
        }

        public static Hash Digest(byte[] byteArray)
        {
            return new Hash(Crypto.Crypto.Digest(byteArray, Length));
        }

        private Hash(byte[] bytes)
        {
            Bytes = bytes;
        }

        public byte[] Bytes { get; }

        public override string ToString()
        {
            return Convert.ToHexString(Bytes).ToLower();
        }

        public string ToShortString()
        {
            var longString = ToString();
            return longString.Substring(0, 6) + "..." + longString.Substring(longString.Length - 6);
        }

        public override bool Equals(object obj) => Equals(obj as Hash);

        public bool Equals(Hash other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Bytes.SequenceEqual(other.Bytes);
        }

        public int CompareTo(Hash other)
        {
            return ToString()
                .CompareTo(other.ToString());
        }

        public override int GetHashCode()
        {
            return ToString()
                .GetHashCode();
        }
    }
}