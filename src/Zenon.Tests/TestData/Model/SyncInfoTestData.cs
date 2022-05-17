using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Zenon.Model.Json;

namespace Zenon.Tests.TestData.Model
{
    internal class SyncInfoTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var json = TestHelper.GetManifestResourceText(typeof(SyncInfoTestData).FullName + ".json");

            dynamic jsonArray = JsonConvert.DeserializeObject(json)!;

            foreach (var item in jsonArray)
            {
                yield return new object[]
                {
                    item.ToString(),
                    typeof(JSyncInfo),
                    null
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
