namespace Zenon.Model.Embedded.Json
{
    public class JLiquidityStakeList
    {
        public string totalAmount { get; set; }
        public string totalWeightedAmount { get; set; }
        public long count { get; set; }
        public JLiquidityStakeEntry[] list { get; set; }
    }
}
