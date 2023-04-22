namespace Zenon.Wallet.Json
{
    public class JKeyFile
    {
        public string baseAddress { get; set; }
        public JCryptoData crypto { get; set; }
        public int timestamp { get; set; }
        public int version { get; set; }
    }
}