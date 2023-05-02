namespace Zenon.Model.Embedded.Json
{
    public class JBridgeNetworkInfo
	{
		public int networkClass { get; set; }
		public int id { get; set; }
		public string name { get; set; }
		public string contractAddress { get; set; }
		public string metadata { get; set; }
		public JTokenPair[] tokenPairs { get; set; }
    }
}
