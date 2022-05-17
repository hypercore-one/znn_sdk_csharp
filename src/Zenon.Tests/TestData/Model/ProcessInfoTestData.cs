using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Zenon.Model.Json;

namespace Zenon.Tests.TestData.Model
{
    internal class ProcessInfoTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var json = TestHelper.GetManifestResourceText(typeof(ProcessInfoTestData).FullName + ".json");

            dynamic jsonArray = JsonConvert.DeserializeObject(json)!;

            foreach (var item in jsonArray)
            {
                yield return new object[]
                {
                    item.ToString(),
                    typeof(JProcessInfo),
                    null
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
