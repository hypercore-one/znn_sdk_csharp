namespace Zenon.Model.NoM.Json
{
    public class JMomentum
    {
        public ulong version { get; set; }
        public ulong chainIdentifier { get; set; }
        public string hash { get; set; }
        public string previousHash { get; set; }
        public ulong height { get; set; }
        public ulong timestamp { get; set; }
        public string data { get; set; }
        public JAccountHeader[] content { get; set; }
        public string changesHash { get; set; }
        public string publicKey { get; set; }
        public string signature { get; set; }
        public string producer { get; set; }
    }
}
