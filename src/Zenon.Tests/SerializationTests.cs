using FluentAssertions;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Zenon.Tests
{
    public partial class ModelTests
    {
        public class Serialization
        {
            [Theory]
            [ClassData(typeof(TestData.Model.OsInfoTestData))]
            [ClassData(typeof(TestData.Model.ProcessInfoTestData))]
            [ClassData(typeof(TestData.Model.NetworkInfoTestData))]
            [ClassData(typeof(TestData.Model.SyncInfoTestData))]
            [ClassData(typeof(TestData.Model.NoM.TokenListTestData))]
            [ClassData(typeof(TestData.Model.NoM.AccountInfoTestData))]
            [ClassData(typeof(TestData.Model.NoM.AccountBlockTestData))]
            [ClassData(typeof(TestData.Model.NoM.AccountBlockListTestData))]
            [ClassData(typeof(TestData.Model.NoM.MomentumTestData))]
            [ClassData(typeof(TestData.Model.NoM.MomentumListTestData))]
            [ClassData(typeof(TestData.Model.NoM.DetailedMomentumListTestData))]
            [ClassData(typeof(TestData.Model.Embedded.ProjectListTestData))]
            [ClassData(typeof(TestData.Model.Embedded.ProjectTestData))]
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
}
