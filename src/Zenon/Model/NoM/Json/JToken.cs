using System.Numerics;

namespace Zenon.Model.NoM.Json
{
    public class JToken
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public string domain { get; set; }
        public string totalSupply { get; set; }
        public long decimals { get; set; }
        public string owner { get; set; }
        public string tokenStandard { get; set; }
        public string maxSupply { get; set; }
        public bool isBurnable { get; set; }
        public bool isMintable { get; set; }
        public bool isUtility { get; set; }
    }
}
