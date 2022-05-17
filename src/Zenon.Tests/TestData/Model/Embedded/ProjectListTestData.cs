using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;

namespace Zenon.Tests.TestData.Model.Embedded
{
    internal class ProjectListTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var json = TestHelper.GetManifestResourceText(typeof(ProjectListTestData).FullName + ".json");

            dynamic jsonArray = JsonConvert.DeserializeObject(json)!;

            foreach (var item in jsonArray)
            {
                yield return new object[]
                {
                    item.ToString(),
                    typeof(JProjectList),
                    typeof(ProjectList)
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
