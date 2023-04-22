using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Zenon.Model.Primitives;

namespace Zenon.Abi
{
    internal class AbiTypeEncodeValidTestData : IEnumerable<object[]>
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

        private static readonly StaticArrayType StaticArrayType = new StaticArrayType("uint8[5]");
        private static readonly DynamicArrayType DynamicArrayType = new DynamicArrayType("uint8[]");

        private const double DoubleMaxValue = 1.1579208923731618E+77;
        private const double DoubleMinValue = -452312848583266388373324160190187140051835877600158453279131187530910662656D;

        public IEnumerator<object[]> GetEnumerator()
        {
            #region Test Boolean
            yield return new object[]
            {
                BooleanType,
                true,
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000001")
            };
            yield return new object[]
            {
                BooleanType,
                "true",
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000001")
            };
            yield return new object[]
            {
                BooleanType,
                "True",
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000001")
            };
            yield return new object[]
            {
                BooleanType,
                false,
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                BooleanType,
                "false",
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                BooleanType,
                "False",
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000000")
            };
            #endregion

            #region Test Integer
            yield return new object[]
            {
                IntType,
                SByte.MinValue,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF80")
            };
            yield return new object[]
            {
                IntType,
                Int16.MinValue,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF8000")
            };
            yield return new object[]
            {
                IntType,
                Int32.MinValue,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF80000000")
            };
            yield return new object[]
            {
                IntType,
                Int64.MinValue,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF8000000000000000")
            };
            yield return new object[]
            {
                IntType,
                Single.MinValue,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000100000000000000000000000000")
            };
            yield return new object[]
            {
                IntType,
                DoubleMinValue,
                Convert.FromHexString("FF00000000000000000000000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                IntType,
                Decimal.MinValue,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000000000000000000000001")
            };
            yield return new object[]
            {
                IntType,
                BigInteger.MinusOne,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
            };
            yield return new object[]
            {
                IntType,
                "0x05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612",
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            yield return new object[]
            {
                IntType,
                "05a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b612",
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            yield return new object[]
            {
                IntType,
                "-123456789",
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF8A432EB")
            };
            yield return new object[]
            {
                IntType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"),
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
            };
            #endregion

            #region Test UnsignedInteger
            yield return new object[]
            {
                UIntType,
                Byte.MaxValue,
                Convert.FromHexString("00000000000000000000000000000000000000000000000000000000000000FF")
            };
            yield return new object[]
            {
                UIntType,
                Int16.MaxValue,
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000007FFF")
            };
            yield return new object[]
            {
                UIntType,
                Int32.MaxValue,
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000007FFFFFFF")
            };
            yield return new object[]
            {
                UIntType,
                Int64.MaxValue,
                Convert.FromHexString("0000000000000000000000000000000000000000000000007FFFFFFFFFFFFFFF")
            };
            yield return new object[]
            {
                UIntType,
                Single.MaxValue,
                Convert.FromHexString("00000000000000000000000000000000FFFFFF00000000000000000000000000")
            };
            yield return new object[]
            {
                UIntType,
                DoubleMaxValue,
                Convert.FromHexString("FFFFFFFFFFFFF800000000000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                UIntType,
                Decimal.MaxValue,
                Convert.FromHexString("0000000000000000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF")
            };
            yield return new object[]
            {
                UIntType,
                BigInteger.Parse("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", NumberStyles.HexNumber),
                Convert.FromHexString("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
            };
            yield return new object[]
            {
                UIntType,
                "0x05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612",
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            yield return new object[]
            {
                UIntType,
                "05a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b612",
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            yield return new object[]
            {
                UIntType,
                "123456789",
                Convert.FromHexString("00000000000000000000000000000000000000000000000000000000075BCD15")
            };
            yield return new object[]
            {
                UIntType,
                Convert.FromHexString("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"),
                Convert.FromHexString("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
            };
            #endregion

            #region Test String
            yield return new object[]
            {
                StringType,
                "Hello Zenon!",
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000000000000C48656C6C6F205A656E6F6E210000000000000000000000000000000000000000")
            };
            #endregion

            #region Test Bytes32
            yield return new object[]
            {
                Bytes32Type,
                Int32.MaxValue,
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000007FFFFFFF")
            };
            yield return new object[]
            {
                Bytes32Type,
                Int64.MaxValue,
                Convert.FromHexString("0000000000000000000000000000000000000000000000007FFFFFFFFFFFFFFF")
            };
            yield return new object[]
            {
                Bytes32Type,
                UInt32.MaxValue,
                Convert.FromHexString("00000000000000000000000000000000000000000000000000000000FFFFFFFF")
            };
            yield return new object[]
            {
                Bytes32Type,
                UInt64.MaxValue,
                Convert.FromHexString("000000000000000000000000000000000000000000000000FFFFFFFFFFFFFFFF")
            };
            yield return new object[]
            {
                Bytes32Type,
                Single.MaxValue,
                Convert.FromHexString("00000000000000000000000000000000FFFFFF00000000000000000000000000")
            };
            yield return new object[]
            {
                Bytes32Type,
                DoubleMaxValue,
                Convert.FromHexString("FFFFFFFFFFFFF800000000000000000000000000000000000000000000000000")
            };
            yield return new object[]
            {
                Bytes32Type,
                Decimal.MaxValue,
                Convert.FromHexString("0000000000000000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF")
            };
            yield return new object[]
            {
                Bytes32Type,
                "05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612",
                Convert.FromHexString("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            yield return new object[]
            {
                Bytes32Type,
                Convert.FromHexString("2020202020202020202020202020202020202020202020202020202020202020"),
                Convert.FromHexString("2020202020202020202020202020202020202020202020202020202020202020")
            };
            #endregion

            #region Test Bytes
            yield return new object[]
            {
                BytesType,
                "05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612",
                Convert.FromHexString("000000000000000000000000000000000000000000000000000000000000002005A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612")
            };
            yield return new object[]
            {
                BytesType,
                Convert.FromHexString("2020202020202020202020202020202020202020202020202020202020202020"),
                Convert.FromHexString("00000000000000000000000000000000000000000000000000000000000000202020202020202020202020202020202020202020202020202020202020202020")
            };
            #endregion

            #region Test TokenStandard
            yield return new object[]
            {
                TokenStandardType,
                TokenStandard.ZnnZts,
                Convert.FromHexString("0000000000000000000000000000000000000000000014E66318C6318C6318C6")
            };
            yield return new object[]
            {
                TokenStandardType,
                TokenStandard.ZnnTokenStandard,
                Convert.FromHexString("0000000000000000000000000000000000000000000014E66318C6318C6318C6")
            };
            #endregion

            #region Test Hash
            yield return new object[]
            {
                HashType,
                Byte.MaxValue,
                Hash.Parse("00000000000000000000000000000000000000000000000000000000000000FF").Bytes
            };
            yield return new object[]
            {
                HashType,
                Int16.MaxValue,
                Hash.Parse("0000000000000000000000000000000000000000000000000000000000007FFF").Bytes
            };
            yield return new object[]
            {
                HashType,
                Int32.MaxValue,
                Hash.Parse("000000000000000000000000000000000000000000000000000000007FFFFFFF").Bytes
            };
            yield return new object[]
            {
                HashType,
                Int64.MaxValue,
                Hash.Parse("0000000000000000000000000000000000000000000000007FFFFFFFFFFFFFFF").Bytes
            };
            yield return new object[]
            {
                HashType,
                Single.MaxValue,
                Hash.Parse("00000000000000000000000000000000FFFFFF00000000000000000000000000").Bytes
            };
            yield return new object[]
            {
                HashType,
                DoubleMaxValue,
                Hash.Parse("FFFFFFFFFFFFF800000000000000000000000000000000000000000000000000").Bytes
            };
            yield return new object[]
            {
                HashType,
                Decimal.MaxValue,
                Hash.Parse("0000000000000000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF").Bytes
            };
            yield return new object[]
            {
                HashType,
                BigInteger.Parse("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", NumberStyles.HexNumber),
                Hash.Parse("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF").Bytes
            };
            yield return new object[]
            {
                HashType,
                "05a0fef85008e63f0680b68d11743ba5caf199994d642590febe570b2a84b612",
                Hash.Parse("05A0FEF85008E63F0680B68D11743BA5CAF199994D642590FEBE570B2A84B612").Bytes
            };
            yield return new object[]
            {
                HashType,
                Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"),
                Hash.Parse("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF").Bytes
            };
            #endregion

            #region Test Address
            yield return new object[]
            {
                AddressType,
                "z1qq0hffeyj0htmnr4gc6grd8zmqfvwzgrydt402",
                Convert.FromHexString("000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903")
            };
            yield return new object[]
            {
                AddressType,
                Address.Parse("z1qq0hffeyj0htmnr4gc6grd8zmqfvwzgrydt402"),
                Convert.FromHexString("000000000000000000000000001F74A72493EEBDCC75463481B4E2D812C70903")
            };
            #endregion

            #region Test Function
            yield return new object[]
            {
                FunctionType,
                new byte[] { 1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4 },
                Convert.FromHexString("0102030405060708090001020304050607080900010203040000000000000000")
            };
            #endregion

            #region Test Array
            yield return new object[]
            {
                DynamicArrayType,
                new int[] { 1,2,3,4,5,6,7,8,9 },
                Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000009000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000003000000000000000000000000000000000000000000000000000000000000000400000000000000000000000000000000000000000000000000000000000000050000000000000000000000000000000000000000000000000000000000000006000000000000000000000000000000000000000000000000000000000000000700000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000009")
            };
            yield return new object[]
            {
                StaticArrayType,
                new int[] { 1,2,3,4,5 },
                Convert.FromHexString("00000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000002000000000000000000000000000000000000000000000000000000000000000300000000000000000000000000000000000000000000000000000000000000040000000000000000000000000000000000000000000000000000000000000005")
            };
            #endregion
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
