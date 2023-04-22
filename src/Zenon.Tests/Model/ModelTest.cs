using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.NoM.Json;

namespace Zenon.Model
{
    public partial class ModelTest
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
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.embedded.project.json", typeof(JProject), typeof(Project))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.embedded.projectList.json", typeof(JProjectList), typeof(ProjectList))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.accountBlock.json", typeof(JAccountBlock), typeof(AccountBlock))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.accountBlockList.json", typeof(JAccountBlockList), typeof(AccountBlockList))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.accountInfo.json", typeof(JAccountInfo), typeof(AccountInfo))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.detailedMomentumList.json", typeof(JDetailedMomentumList), typeof(DetailedMomentumList))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.momentum.json", typeof(JMomentum), typeof(Momentum))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.momentumList.json", typeof(JMomentumList), typeof(MomentumList))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.tokenList.json", typeof(JTokenList), typeof(TokenList))]
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
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.embedded.project.json", typeof(JProject), typeof(Project))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.embedded.projectList.json", typeof(JProjectList), typeof(ProjectList))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.accountBlock.json", typeof(JAccountBlock), typeof(AccountBlock))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.accountBlockList.json", typeof(JAccountBlockList), typeof(AccountBlockList))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.accountInfo.json", typeof(JAccountInfo), typeof(AccountInfo))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.detailedMomentumList.json", typeof(JDetailedMomentumList), typeof(DetailedMomentumList))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.momentum.json", typeof(JMomentum), typeof(Momentum))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.momentumList.json", typeof(JMomentumList), typeof(MomentumList))]
        [MemberData(nameof(GetArguments), "Zenon.Resources.model.nom.tokenList.json", typeof(JTokenList), typeof(TokenList))]
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