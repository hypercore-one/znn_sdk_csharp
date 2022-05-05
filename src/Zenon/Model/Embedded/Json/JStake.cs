namespace Zenon.Model.Embedded.Json
{
    public class JStakeList
    {
        public long totalAmount { get; set; }
        public long totalWeightedAmount { get; set; }
        public long count { get; set; }
        public JStakeEntry[] list { get; set; }
    }
}
