using Zenon.Model.Primitives.Json;

namespace Zenon.Model.NoM.Json
{
    public class JAccountBlockTemplate
    {
        public int version { get; set; }
        public int chainIdentifier { get; set; }
        public int blockType { get; set; }
        public string hash { get; set; }
        public string previousHash { get; set; }
        public long height { get; set; }
        public JHashHeight momentumAcknowledged { get; set; }
        public string address { get; set; }
        public string toAddress { get; set; }
        public string amount { get; set; }
        public string tokenStandard { get; set; }
        public string fromBlockHash { get; set; }
        public string data { get; set; }
        public long fusedPlasma { get; set; }
        public long difficulty { get; set; }
        public string nonce { get; set; }
        public string publicKey { get; set; }
        public string signature { get; set; }
    }
}
