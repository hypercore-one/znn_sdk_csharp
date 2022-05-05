using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Zenon.Tests.TestData
{
    internal class TokenTestData : IEnumerable<object[]>
    {
        internal static string GetManifestResourceText(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream!))
                return reader.ReadToEnd();
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            var json = GetManifestResourceText(Assembly.GetExecutingAssembly(), "Zenon.Tests.TestData.TokenTestData.json");

            dynamic tokens = JsonConvert.DeserializeObject(json)!;

            foreach (var token in tokens)
            {
                yield return new object[]
                {
                    token.ToString()
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
