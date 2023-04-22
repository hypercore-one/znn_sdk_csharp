using FluentAssertions;
using System.Collections;
using System.Numerics;
using System.Text;
using Xunit;
using Zenon.Embedded.TestData;
using Zenon.Model.Primitives;

namespace Zenon.Embedded
{
    public class DefinitionsTest
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
                else if (arg is BigInteger)
                {
                    builder.Append(arg.ToString()!.ToLower());
                }
                else
                {
                    builder.Append(arg.ToString()!.ToLower());
                }
            }
            return $"{{{builder}}}";
        }

        [Theory]
        [ClassData(typeof(AbiFunctionValidTestData))]
        public void When_DecodeFunction_ExpectToEqual(Abi.Abi definition, string name, object[] args, byte[] expectedResult)
        {
            // Execute
            var result = definition.DecodeFunction(expectedResult);

            // Validate
            ArgumentsToString(result).Should().BeEquivalentTo(ArgumentsToString(args));
        }

        [Theory]
        [ClassData(typeof(AbiFunctionValidTestData))]
        public void When_EncodeFunction_ExpectToEqual(Abi.Abi definition, string name, object[] args, byte[] expectedResult)
        {
            // Execute
            var result = definition.EncodeFunction(name, args);

            // Validate
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
