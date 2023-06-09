namespace Zenon.Model.Embedded.Json
{
    public class JLiquidityStakeEntry
    {
        public string amount { get; set; }
        public string tokenStandard { get; set; }
        public string weightedAmount { get; set; }
        public long startTime { get; set; }
        public long revokeTime { get; set; }
        public long expirationTime { get; set; }
    }
}
