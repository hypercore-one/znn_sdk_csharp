using Zenon.Model.Primitives.Json;

namespace Zenon.Model.NoM.Json
{
    public class JAccountBlockTemplate
    {
        public ulong version { get; set; }
        public ulong chainIdentifier { get; set; }
        public ulong blockType { get; set; }
        public string hash { get; set; }
        public string previousHash { get; set; }
        public ulong height { get; set; }
        public JHashHeight momentumAcknowledged { get; set; }
        public string address { get; set; }
        public string toAddress { get; set; }
        public string amount { get; set; }
        public string tokenStandard { get; set; }
        public string fromBlockHash { get; set; }
        public string data { get; set; }
        public ulong fusedPlasma { get; set; }
        public ulong difficulty { get; set; }
        public string nonce { get; set; }
        public string publicKey { get; set; }
        public string signature { get; set; }
    }
}
