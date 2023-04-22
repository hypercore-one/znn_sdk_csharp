namespace Zenon.Model.Embedded.Json
{
    public class JSentinelInfo
    {
        public string owner { get; set; }
        public long registrationTimestamp { get; set; }
        public bool isRevocable { get; set; }
        public long revokeCooldown { get; set; }
        public bool active { get; set; }
    }
}
