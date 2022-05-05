namespace Zenon.Model.Json
{
    public class JPeer
    {
        public string publicKey { get; set; }
        public string ip { get; set; }
    }

    public class JNetworkInfo
    {
        public long numPeers { get; set; }
        public JPeer self { get; set; }
        public JPeer[] peers { get; set; }
    }

    public class JProcessInfo
    {
        public string commit { get; set; }
        public string version { get; set; }
    }

    public class JOsInfo
    {
        public string os { get; set; }
        public string platform { get; set; }
        public string platformVersion { get; set; }
        public string kernelVersion { get; set; }
        public long memoryTotal { get; set; }
        public long memoryFree { get; set; }
        public long numCPU { get; set; }
        public long numGoroutine { get; set; }
    }

    public class JSyncInfo
    {
        public long state { get; set; }
        public long currentHeight { get; set; }
        public long targetHeight { get; set; }
    }
}
