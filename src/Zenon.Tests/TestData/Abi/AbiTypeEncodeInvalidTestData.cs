using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Zenon.Abi;
using Zenon.Model.Primitives;

namespace Zenon.Tests.TestData
{
    internal class AbiTypeEncodeInvalidTestData : IEnumerable<object[]>
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

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                BooleanType,
                Int32.MaxValue
            };
            yield return new object[]
            {
                IntType,
                "NotANumber"
            };
            yield return new object[]
            {
                UIntType,
                BigInteger.MinusOne
            };
            yield return new object[]
            {
                Bytes32Type,
                Hash.Empty
            };
            yield return new object[]
            {
                BytesType,
                Hash.Empty
            };
            yield return new object[]
            {
                StringType,
                Int32.MaxValue
            };
            yield return new object[]
            {
                FunctionType,
                "Function"
            };
            yield return new object[]
            {
                TokenStandardType,
                TokenStandard.ZnnZts.Bytes
            };
            yield return new object[]
            {
                HashType,
                Hash.Empty
            };
            yield return new object[]
            {
                AddressType,
                Address.Parse("z1qq0hffeyj0htmnr4gc6grd8zmqfvwzgrydt402").Bytes
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
