namespace Zenon.Model.NoM.Json
{
    public class JMomentum
    {
        public int version { get; set; }
        public int chainIdentifier { get; set; }
        public string hash { get; set; }
        public string previousHash { get; set; }
        public long height { get; set; }
        public long timestamp { get; set; }
        public string data { get; set; }
        public JAccountHeader[] content { get; set; }
        public string changesHash { get; set; }
        public string publicKey { get; set; }
        public string signature { get; set; }
        public string producer { get; set; }
    }
}
