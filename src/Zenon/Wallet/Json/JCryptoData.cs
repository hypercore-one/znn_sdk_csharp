namespace Zenon.Wallet.Json
{
    public class JCryptoData
    {
        public JArgon2Params argon2Params { get; set;}
        public string cipherData { get; set; }
        public string cipherName { get; set; }
        public string kdf { get; set; }
        public string nonce { get; set; }
    }
}
