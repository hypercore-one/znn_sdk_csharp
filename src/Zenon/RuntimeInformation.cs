using System.Runtime.InteropServices;

namespace Zenon
{
    public interface IRuntimeInformation
    {
        bool IsOSPlatform(OSPlatform osPlatform);
    }

    public class DotNetRuntimeInformation : IRuntimeInformation
    {
        public bool IsOSPlatform(OSPlatform osPlatform)
        {
            return RuntimeInformation.IsOSPlatform(osPlatform);
        }
    }
}
