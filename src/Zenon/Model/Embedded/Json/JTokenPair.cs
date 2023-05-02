namespace Zenon.Model.Embedded.Json
{
    public class JTokenPair
	{
		public string tokenStandard { get; set; }
		public string tokenAddress { get; set; }
		public bool bridgeable { get; set; }
		public bool redeemable { get; set; }
		public bool owned { get; set; }
		public long minAmount { get; set; }
		public int feePercentage { get; set; }
		public int redeemDelay { get; set; }
		public string metadata { get; set; }
    }
}