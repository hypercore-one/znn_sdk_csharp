namespace Zenon.Model.Embedded.Json
{
    public class JWrapTokenRequest
    {
        public int networkClass { get; set; }
        public int chainId { get; set; }
        public string id { get; set; }
        public string toAddress { get; set; }
        public string tokenStandard { get; set; }
        public string tokenAddress { get; set; }
        public string amount { get; set; }
        public string fee { get; set; }
        public string signature { get; set; }
        public long creationMomentumHeight { get; set; }
    }
}
