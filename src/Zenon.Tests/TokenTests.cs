using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Zenon.Model.NoM;
using Zenon.Model.NoM.Json;
using Zenon.Tests.TestData;

namespace Zenon.Tests
{
    public class TokenTests
    {
        [Theory]
        [ClassData(typeof(TokenTestData))]
        public void When_Deserialize_ExpectToEqual(string expectedJson)
        {
            // Execute
            var token = new Token(JsonConvert.DeserializeObject<JToken>(expectedJson));
            var json = JsonConvert.SerializeObject(token.ToJson(), Formatting.Indented);

            // Validate
            expectedJson.Should().Be(json);
        }
    }
}
