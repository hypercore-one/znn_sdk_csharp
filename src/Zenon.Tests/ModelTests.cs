using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Reflection;
using Xunit;

namespace Zenon.Tests
{
    public partial class ModelTests
    {
        [Theory]
        [ClassData(typeof(TestData.Model.Embedded.ProjectListTestData))]
        [ClassData(typeof(TestData.Model.Embedded.ProjectTestData))]
        [ClassData(typeof(TestData.Model.NoM.AccountBlockListTestData))]
        [ClassData(typeof(TestData.Model.NoM.AccountBlockListTestData))]
        [ClassData(typeof(TestData.Model.NoM.AccountBlockTestData))]
        [ClassData(typeof(TestData.Model.NoM.AccountInfoTestData))]
        [ClassData(typeof(TestData.Model.NoM.DetailedMomentumListTestData))]
        [ClassData(typeof(TestData.Model.NoM.MomentumListTestData))]
        [ClassData(typeof(TestData.Model.NoM.MomentumTestData))]
        [ClassData(typeof(TestData.Model.NoM.TokenListTestData))]
        public void When_CreateModel_ExpectSuccess(string originalJson, Type dataType, Type modelType)
        {
            // Setup
            var data = JsonConvert.DeserializeObject(originalJson, dataType);

            // Execute
            Action action = () => Activator.CreateInstance(modelType, data);

            // Validate
            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(TestData.Model.Embedded.ProjectListTestData))]
        [ClassData(typeof(TestData.Model.Embedded.ProjectTestData))]
        [ClassData(typeof(TestData.Model.NoM.AccountBlockListTestData))]
        [ClassData(typeof(TestData.Model.NoM.AccountBlockTestData))]
        [ClassData(typeof(TestData.Model.NoM.AccountInfoTestData))]
        [ClassData(typeof(TestData.Model.NoM.DetailedMomentumListTestData))]
        [ClassData(typeof(TestData.Model.NoM.MomentumListTestData))]
        [ClassData(typeof(TestData.Model.NoM.MomentumTestData))]
        [ClassData(typeof(TestData.Model.NoM.TokenListTestData))]
        public void When_ConvertModel_ExpectSuccess(string originalJson, Type dataType, Type modelType)
        {
            // Setup
            var data = JsonConvert.DeserializeObject(originalJson, dataType);
            var toJson = modelType.GetMethod("ToJson", BindingFlags.Instance | BindingFlags.Public, Type.EmptyTypes)!;

            // Execute
            Action action = () =>
            {
                var model = Activator.CreateInstance(modelType, data);
                var json = toJson.Invoke(model, new object[0]);
            };

            // Validate
            action.Should().NotThrow();
        }
    }
}
