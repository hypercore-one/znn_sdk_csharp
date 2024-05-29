namespace Zenon.Model.Embedded.Json
{
    public class JBridgeInfo
    {
        public string administrator { get; set; }
        public string compressedTssECDSAPubKey { get; set; }
        public string decompressedTssECDSAPubKey { get; set; }
        public bool allowKeyGen { get; set; }
        public bool halted { get; set; }
        public ulong unhaltedAt { get; set; }
        public ulong unhaltDurationInMomentums { get; set; }
        public ulong tssNonce { get; set; }
        public string metadata { get; set; }
    }
}
