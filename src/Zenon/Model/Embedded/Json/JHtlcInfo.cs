namespace Zenon.Model.Embedded.Json
{
    public class JHtlcInfo
    {
        public string id { get; set; }
        public string timeLocked { get; set; }
        public string hashLocked { get; set; }
        public string tokenStandard { get; set; }
        public string amount { get; set; }
        public long expirationTime { get; set; }
        public int hashType { get; set; }
        public int keyMaxSize { get; set; }
        public string hashLock { get; set; }
    }
}
