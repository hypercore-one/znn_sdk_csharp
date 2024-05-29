namespace Zenon.Model.Embedded.Json
{
    public class JUnwrapTokenRequest
    {
        public ulong registrationMomentumHeight { get; set; }
        public uint networkClass { get; set; }
        public string transactionHash { get; set; }
        public uint logIndex { get; set; }
        public uint chainId { get; set; }
        public string toAddress { get; set; }
        public string tokenStandard { get; set; }
        public string tokenAddress { get; set; }
        public string amount { get; set; }
        public string signature { get; set; }
        public int redeemed { get; set; }
        public int revoked { get; set; }
    }
}