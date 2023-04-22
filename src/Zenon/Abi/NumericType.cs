using System;
using System.Globalization;
using System.Numerics;
using Zenon.Utils;

namespace Zenon.Abi
{
    public abstract class NumericType : AbiType
    {
        public static BigInteger ToBigInt(object value)
        {
            if (value is string)
            {
                var str = (string)value;

                if (str.StartsWith("0x"))
                {
                    return BigInteger.Parse(str.Substring(2), NumberStyles.HexNumber);
                }
                else if (str.Contains('a') ||
                    str.Contains('b') ||
                    str.Contains('c') ||
                    str.Contains('d') ||
                    str.Contains('e') ||
                    str.Contains('f'))
                {
                    return BigInteger.Parse(str, NumberStyles.HexNumber);
                }
                return BigInteger.Parse(str);
            }
            else if (value is BigInteger)
            {
                return (BigInteger)value;
            }
            else if (value is byte)
            {
                return new BigInteger((byte)value);
            }
            else if (value is ushort)
            {
                return new BigInteger((ushort)value);
            }
            else if (value is uint)
            {
                return new BigInteger((uint)value);
            }
            else if (value is ulong)
            {
                return new BigInteger((ulong)value);
            }
            else if (value is sbyte)
            {
                return new BigInteger((sbyte)value);
            }
            else if (value is short)
            {
                return new BigInteger((short)value);
            }
            else if (value is int)
            {
                return new BigInteger((int)value);
            }
            else if (value is long)
            {
                return new BigInteger((long)value);
            }
            else if (value is float)
            {
                return new BigInteger((float)value);
            }
            else if (value is double)
            {
                return new BigInteger((double)value);
            }
            else if (value is decimal)
            {
                return new BigInteger((decimal)value);
            }
            else if (value is byte[])
            {
                return BytesUtils.BytesToBigInt((byte[])value);
            }
            else
            {
                throw new NotSupportedException($"Value type '{value.GetType().Name}' is not supported.");
            }
        }
        public NumericType(string name)
            : base(name)
        { }

        protected BigInteger EncodeInternal(object value)
        {
            return ToBigInt(value);
        }
    }
}
