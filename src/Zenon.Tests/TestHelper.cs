using System.IO;
using System.Reflection;

namespace Zenon.Tests
{
    internal static class TestHelper
    {
        internal static string GetManifestResourceText(string resourceName)
        {
            return GetManifestResourceText(Assembly.GetCallingAssembly(), resourceName);
        }

        internal static string GetManifestResourceText(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream!))
                return reader.ReadToEnd();
        }
    }
}
