using FluentAssertions;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using Xunit;
using Zenon.Model.Primitives;
using Zenon.Tests.TestData;

namespace Zenon.Tests
{
    public class AbiTests
    {
        private static string ArgumentsToString(IEnumerable args)
        {
            var builder = new StringBuilder();
            foreach (var arg in args)
            {
                var type = arg.GetType();

                if (builder.Length > 0)
                    builder.Append(",");

                if (type.IsArray)
                {
                    builder.Append(ArgumentsToString((IEnumerable)arg)!.ToLower());
                }
                else if (arg is Hash)
                {
                    builder.Append(ArgumentsToString(((Hash)arg).Bytes)!.ToLower());
                }
                else
                {
                    builder.Append(arg.ToString()!.ToLower());
                }
            }
            return $"{{{builder}}}";
        }

        [Theory]
        [ClassData(typeof(AbiFunctionTestData))]
        public void When_EncodeFunction_ExpectToEqual(Abi.Abi definition, string name, object[] args, byte[] expectedEncoded)
        {
            // Execute
            var encoded = definition.EncodeFunction(name, args);

            // Validate
            expectedEncoded.Should().BeEquivalentTo(encoded);
        }

        [Theory]
        [ClassData(typeof(AbiFunctionTestData))]
        public void When_DecodeFunction_ExpectToEqual(Abi.Abi definition, string name, object[] args, byte[] encoded)
        {
            // Execute
            var decoded = definition.DecodeFunction(encoded);

            // Validate
            ArgumentsToString(args).Should().Be(ArgumentsToString(decoded));
        }
    }
}
