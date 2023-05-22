using Konscious.Security.Cryptography;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using Zenon.Model.Primitives;
using Zenon.Utils;
using Zenon.Wallet.Json;

namespace Zenon.Wallet
{
    public class KeyFile
    {
        public static KeyFile Encrypt(KeyStore store, string password)
        {
            if (!AesGcm.IsSupported)
                throw new NotSupportedException("AES-GCM Encryption is not supported on this platform.");

            DateTimeOffset eposh = DateTimeOffset.UnixEpoch;
            DateTimeOffset now = DateTimeOffset.UtcNow;
            TimeSpan ts = now.Subtract(eposh);

            var timestamp = (int)Math.Round(ts.TotalMilliseconds / 1000);

            var stored = new KeyFile(store.GetKeyPair().Address,
                new CryptoData(new Argon2Params(new byte[0]), new byte[0], "aes-256-gcm", "argon2.IDKey", new byte[0]),
                timestamp, 1);

            return stored.EncryptEntropy(store, password);
        }

        public KeyStore Decrypt(string password)
        {
            try
            {
                // Create password hash using Argon2id hasing algorithm.
                var key = new Argon2id(Encoding.UTF8.GetBytes(password))
                {
                    Salt = this.Crypto.Argon2Params.Salt,
                    Iterations = 1, // Number of iterations to perform
                    MemorySize = 64 * 1024, // Amount of memory (in kilobytes) to use
                    DegreeOfParallelism = 4 // Degree of parallelism (i.e. number of threads)
                }
                .GetBytes(32); // Using AES-256-GCM, you’ll need a key 256-bits (32-bytes) in length.

                using (var aes = new AesGcm(key))
                {
                    var entropy = new byte[this.Crypto.CipherData.Length - 16];

                    aes.Decrypt(
                        this.Crypto.Nonce,
                        this.Crypto.CipherData.Sublist(0, this.Crypto.CipherData.Length - 16),
                        this.Crypto.CipherData.Sublist(this.Crypto.CipherData.Length - 16, this.Crypto.CipherData.Length),
                        entropy,
                        Encoding.UTF8.GetBytes("zenon"));

                    return KeyStore.FromEntropy(BytesUtils.ToHexString(entropy));
                }
            }
            catch (CryptographicException)
            {
                throw new IncorrectPasswordException();
            }
        }

        private static byte[] FromHexString(string value)
        {
            return BytesUtils.FromHexString(value.Substring(2));
        }

        private static string ToHexString(byte[] bytes)
        {
            return "0x" + BytesUtils.ToHexString(bytes);
        }

        private class CryptoData
        {
            public CryptoData(JCryptoData json)
            {
                this.Argon2Params = json.argon2Params != null
                    ? new Argon2Params(json.argon2Params)
                    : null;
                this.CipherData = FromHexString(json.cipherData);
                this.CipherName = json.cipherName;
                this.Kdf = json.kdf;
                this.Nonce = FromHexString(json.nonce);
            }

            public CryptoData(Argon2Params argon2Params, byte[] cipherData, string cipherName, string kdf, byte[] nonce)
            {
                this.Argon2Params = argon2Params;
                this.CipherData = cipherData;
                this.CipherName = cipherName;
                this.Kdf = kdf;
                this.Nonce = nonce;
            }

            public Argon2Params Argon2Params { get; }
            public byte[] CipherData { get; internal set; }
            public string CipherName { get; }
            public string Kdf { get; }
            public byte[] Nonce { get; internal set; }

            public JCryptoData ToJson()
            {
                return new JCryptoData()
                {
                    argon2Params = this.Argon2Params?.ToJson(),
                    cipherData = ToHexString(this.CipherData),
                    cipherName = this.CipherName,
                    kdf = this.Kdf,
                    nonce = ToHexString(this.Nonce)
                };
            }
        }

        private class Argon2Params
        {
            public Argon2Params(JArgon2Params json)
            {
                this.Salt = FromHexString(json.salt);
            }

            public Argon2Params(byte[] salt)
            {
                this.Salt = salt;
            }

            public byte[] Salt { get; internal set; }

            public JArgon2Params ToJson()
            {
                return new JArgon2Params()
                {
                    salt = ToHexString(this.Salt)
                };
            }
        }

        public KeyFile(JKeyFile json)
        {
            BaseAddress = Address.Parse(json.baseAddress);
            Crypto = json.crypto != null ? new CryptoData(json.crypto) : null;
            Timestamp = json.timestamp;
            Version = json.version;
        }

        private KeyFile(Address baseAddress, CryptoData crypto, int timestamp, int version)
        {
            this.BaseAddress = baseAddress;
            this.Crypto = crypto;
            this.Timestamp = timestamp;
            this.Version = version;
        }

        public Address BaseAddress { get; }
        private CryptoData Crypto { get; }
        public int Timestamp { get; }
        public int Version { get; }

        private KeyFile EncryptEntropy(KeyStore store, string password)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                // Salt (16 bytes recommended for password hashing)
                var salt = new byte[16];
                generator.GetBytes(salt);

                // Create password hash using Argon2id hasing algorithm.
                var key = new Argon2id(Encoding.UTF8.GetBytes(password))
                {
                    Salt = salt,
                    Iterations = 1, // Number of iterations to perform
                    MemorySize = 64 * 1024, // Amount of memory (in kibibytes) to use
                    DegreeOfParallelism = 4 // Degree of parallelism (i.e. number of threads)
                }
                .GetBytes(32); // Using AES-256-GCM, you’ll need a key 256-bits (32-bytes) in length.

                // If you are using .NET Core 3 onwards, you can use the 
                // AES-GCM implementation found in System.Security.Cryptography.
                // On Windows and Linux, this API will call into the 
                // OS implementations of AES, while macOS will require you to 
                // have OpenSSL installed.
                using (var aes = new AesGcm(key))
                {
                    // To encrypt, you’ll first need to generate a nonce: a number used once.
                    // This must be a cryptographically random value unique to this operation.
                    // You must never re-use a nonce for the same key; otherwise, you will destroy
                    // the security of this encryption algorithm.
                    // You’ll also see “nonce” referred to as the Initialization Vector (IV).
                    //
                    // For AES-GCM, the nonce must be 96-bits (12-bytes) in length.
                    var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
                    generator.GetBytes(nonce);

                    // Convert plaintext (the value you want to encrypt) into bytes and create
                    // destinations for the resulting ciphertext and tag (the authentication tag, also known as the MAC).
                    var ciphertext = new byte[key.Length];
                    var tag = new byte[AesGcm.TagByteSizes.MaxSize];

                    aes.Encrypt(nonce, BytesUtils.FromHexString(store.Entropy), ciphertext, tag, Encoding.UTF8.GetBytes("zenon"));

                    this.Crypto.CipherData = ArrayUtils.Concat(ciphertext, tag);
                    this.Crypto.Nonce = nonce;
                    this.Crypto.Argon2Params.Salt = salt;

                    return this;
                }
            }
        }

        public JKeyFile ToJson()
        {
            return new JKeyFile()
            {
                baseAddress = this.BaseAddress.ToString(),
                crypto = this.Crypto?.ToJson(),
                timestamp = this.Timestamp,
                version = this.Version
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this.ToJson(), Formatting.Indented);
        }
    }
}
