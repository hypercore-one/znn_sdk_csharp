using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Zenon.Model.NoM;
using Zenon.Model.NoM.Json;

namespace Zenon.Tests.TestData.Model.NoM
{
    internal class MomentumTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var json = TestHelper.GetManifestResourceText(typeof(MomentumTestData).FullName + ".json");

            dynamic jsonArray = JsonConvert.DeserializeObject(json)!;

            foreach (var item in jsonArray)
            {
                yield return new object[]
                {
                    item.ToString(),
                    typeof(JMomentum),
                    typeof(Momentum)
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
