namespace Zenon.Model.Embedded.Json
{
    public class JUnwrapTokenRequest
    {
        public long registrationMomentumHeight { get; set; }
        public int networkClass { get; set; }
        public string transactionHash { get; set; }
        public int logIndex { get; set; }
        public int chainId { get; set; }
        public string toAddress { get; set; }
        public string tokenStandard { get; set; }
        public string tokenAddress { get; set; }
        public long amount { get; set; }
        public string signature { get; set; }
        public int redeemed { get; set; }
        public int revoked { get; set; }
    }
}