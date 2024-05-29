namespace Zenon.Model.Embedded.Json
{
    public class JWrapTokenRequest
    {
        public uint networkClass { get; set; }
        public uint chainId { get; set; }
        public string id { get; set; }
        public string toAddress { get; set; }
        public string tokenStandard { get; set; }
        public string tokenAddress { get; set; }
        public string amount { get; set; }
        public string fee { get; set; }
        public string signature { get; set; }
        public ulong creationMomentumHeight { get; set; }
    }
}
