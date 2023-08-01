namespace Zenon.Model.Embedded.Json
{
    public class JStakeList
    {
        public string totalAmount { get; set; }
        public string totalWeightedAmount { get; set; }
        public long count { get; set; }
        public JStakeEntry[] list { get; set; }
    }
}
