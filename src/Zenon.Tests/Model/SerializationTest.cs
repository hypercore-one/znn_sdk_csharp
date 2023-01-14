using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using Zenon.Model.Json;

namespace Zenon.Model
{
    public class SerializationTest
    {
        public static IEnumerable<object[]> GetArguments(string resourceName, Type dataType, Type modelType)
        {
            var json = TestHelper.GetManifestResourceText(resourceName);

            dynamic jsonArray = JsonConvert.DeserializeObject(json)!;

            foreach (var item in jsonArray)
            {
                yield return new object[]
                {
                    item.ToString(),
                    dataType,
                    modelType
                };
            }
        }

        [Theory]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.osInfo.json", typeof(JOsInfo), null)]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.processInfo.json", typeof(JProcessInfo), null)]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.networkInfo.json", typeof(JNetworkInfo), null)]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.syncInfo.json", typeof(JSyncInfo), null)]
        public void When_Deserialize_ExpectToEqual(string originalJson, Type dataType, Type modelType)
        {
            // Setup
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include
            };

            // Execute
            var data = JsonConvert.DeserializeObject(originalJson, dataType);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            // Validate
            json.Should().Be(originalJson);
        }
    }
}
