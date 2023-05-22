namespace Zenon.Model.Embedded.Json
{
    public class JLiquidityStakeEntry
    {
        public long amount { get; set; }
        public string tokenStandard { get; set; }
        public long weightedAmount { get; set; }
        public long startTime { get; set; }
        public long revokeTime { get; set; }
        public long expirationTime { get; set; }
    }
}
