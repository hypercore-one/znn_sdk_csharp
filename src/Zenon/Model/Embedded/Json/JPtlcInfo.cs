namespace Zenon.Model.Embedded.Json
{
    public class JPtlcInfo
    {
        public string id { get; set; }
        public string timeLocked { get; set; }
        public string tokenStandard { get; set; }
        public long amount { get; set; }
        public long expirationTime { get; set; }
        public int pointType { get; set; }
        public string pointLock { get; set; }
    }
}
