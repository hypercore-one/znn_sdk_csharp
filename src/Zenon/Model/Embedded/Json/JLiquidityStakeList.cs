namespace Zenon.Model.Embedded.Json
{
    public class JLiquidityStakeList
    {
        public long totalAmount { get; set; }
        public long totalWeightedAmount { get; set; }
        public long count { get; set; }
        public JLiquidityStakeEntry[] list { get; set; }
    }
}
