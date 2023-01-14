using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Zenon.Model.Primitives;

namespace Zenon.Abi
{
    internal class AbiTypeDecodeValidTestData : IEnumerable<object[]>
    {
        private static readonly BoolType BooleanType = new BoolType();
        private static readonly IntType IntType = new IntType("int256");
        private static readonly UnsignedIntType UIntType = new UnsignedIntType("uint256");
        private static readonly StringType StringType = new StringType();
        private static readonly Bytes32Type Bytes32Type = new Bytes32Type("bytes32");
        private static readonly BytesType BytesType = new BytesType("bytes");
        private static readonly FunctionType FunctionType = new FunctionType();
        private static readonly TokenStandardType TokenStandardType = new TokenStandardType();
        private static readonly HashType HashType = new HashType("hash");
        private static readonly AddressType AddressType = new AddressType();

        private const double DoubleMaxValue = 1.1579208923731618E+77;
        private const double DoubleMinValue = -452312848583266388373324160190187140051835877600158453279131187530910662656D;

        public IEnumerator<object[]> GetEnumerator()
        {
            #region Test Boolean
            yield return new object[]
            {
                BooleanType,
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000001"),
                true
            };
            yield return new object[]
            {
                BooleanType,
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000000"),
                false
            };
            #endregion

            #region Test Integer
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF80"),
                new BigInteger(SByte.MinValue)
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF8000"),
                new BigInteger(Int16.MinValue)
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF80000000"),
                new BigInteger(Int32.MinValue)
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF8000000000000000"),
                new BigInteger(Int64.MinValue)
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000100000000000000000000000000"),
                new BigInteger(Single.MinValue)
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FF00000000000000000000000000000000000000000000000000000000000000"),
                new BigInteger(DoubleMinValue)
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000000000000000000000001"),
                new BigInteger(Decimal.MinValue)
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"),
                BigInteger.MinusOne
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612"),
                BigInteger.Parse("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612", NumberStyles.HexNumber)
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF8A432EB"),
                BigInteger.Parse("-123456789")
            };
            #endregion

            #region Test UnsignedInteger
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("00000000000000000000000000000000000000000000000000000000000000FF"),
                new BigInteger(Byte.MaxValue)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000007FFF"),
                new BigInteger(Int16.MaxValue)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000007FFFFFFF"),
                new BigInteger(Int32.MaxValue)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("0000000000000000000000000000000000000000000000007FFFFFFFFFFFFFFF"),
                new BigInteger(Int64.MaxValue)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("00000000000000000000000000000000FFFFFF00000000000000000000000000"),
                new BigInteger(Single.MaxValue)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("FFFFFFFFFFFFF800000000000000000000000000000000000000000000000000"),
                new BigInteger(DoubleMaxValue)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("0000000000000000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF"),
                new BigInteger(Decimal.MaxValue)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"),
                BigInteger.Parse("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", NumberStyles.HexNumber)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612"),
                BigInteger.Parse("05a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b612", NumberStyles.HexNumber)
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("00000000000000000000000000000000000000000000000000000000075BCD15"),
                BigInteger.Parse("123456789")
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"),
                new BigInteger(Enumerable.Repeat<byte>(0xFF, 32).ToArray(), true)
            };
            #endregion

            #region Test String
            yield return new object[]
            {
                StringType,
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000000000000C48656C6C6F205A656E6F6E210000000000000000000000000000000000000000"),
                "Hello Zenon!"
            };
            #endregion

            #region Test Bytes32
            yield return new object[]
            {
                Bytes32Type,
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000007FFFFFFF"),
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000007FFFFFFF")
            };
            yield return new object[]
            {
                Bytes32Type,
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612"),
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            #endregion

            #region Test Bytes
            yield return new object[]
            {
                BytesType,
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000000000002005A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612"),
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            #endregion

            #region Test TokenStandard
            yield return new object[]
            {
                TokenStandardType,
                Convert.FromHexString("0000000000000000000000000000000000000000000014E66318C6318C6318C6"),
                TokenStandard.ZnnZts,

            };
            #endregion

            #region Test Hash
            yield return new object[]
            {
                HashType,
                Convert.FromHexString("05a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b612"),
                Hash.Parse("05a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b612")
            };
            #endregion

            #region Test Address
            yield return new object[]
            {
                AddressType,
                Convert.FromHexString("000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903"),
                Address.Parse("z1qq0hffeyj0htmnr4gc6grd8zmqfvwzgrydt402")
            };
            #endregion
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
