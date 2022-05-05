namespace Zenon.Model.Embedded.Json
{
    public class JStakeEntry
    {
        public long amount { get; set; }
        public long weightedAmount { get; set; }
        public long startTimestamp { get; set; }
        public long expirationTimestamp { get; set; }
        public string address { get; set; }
        public string id { get; set; }
    }
}
