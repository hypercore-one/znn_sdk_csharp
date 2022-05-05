using FluentAssertions;
using Newtonsoft.Json;
using System;
using Xunit;
using Zenon.Model.Primitives;
using Zenon.Model.Primitives.Json;

namespace Zenon.Tests
{
    public class HashHeightTests
    {
        [Theory]
        [InlineData("b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9", 259, "b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b90000000000000103")]
        public void When_GetBytes_ExpectToEqual(string hashString, long? height, string byteString)
        {
            // Setup
            var hh = new HashHeight(Hash.Parse(hashString), height);

            // Execute
            var bytes = hh.GetBytes();

            // Validate
            bytes.Should().BeEquivalentTo(Convert.FromHexString(byteString));
        }

        [Theory]
        [InlineData("b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9", 259, "{\"hash\":\"b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9\",\"height\":259}")]
        [InlineData("b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9", null, "{\"hash\":\"b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9\",\"height\":null}")]
        public void When_Serialize_ExpectToEqual(string hashString, long? height, string expectedJson)
        {
            // Setup
            var hh = new HashHeight(Hash.Parse(hashString), height);

            // Execute
            string json = JsonConvert.SerializeObject(hh.ToJson(), Formatting.None);

            // Validate
            json.Should().Be(expectedJson);
        }

        [Theory]
        [InlineData("b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9", 259, "{\"hash\":\"b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9\",\"height\":259}")]
        [InlineData("b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9", null, "{\"hash\":\"b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9\",\"height\":null}")]
        public void When_Deserialize_ExpectToEqual(string hashString, long? height, string json)
        {
            // Setup
            var exprectedHh = new HashHeight(Hash.Parse(hashString), height);

            // Execute
            var hh = new HashHeight(JsonConvert.DeserializeObject<JHashHeight>(json));

            // Validate
            hh.Hash.Should().Be(exprectedHh.Hash);
            hh.Height.Should().Be(exprectedHh.Height);
        }
    }
}
