using FluentAssertions;
using Xunit;
using Zenon.Wallet.Json;

namespace Zenon.Wallet
{
    public class KeyFileFixture
    {
        public KeyFileFixture()
        {
            Mnemonic = "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";
            Password = "Secret";
            KeyFileJson = new JKeyFile()
            {
                baseAddress = "z1qqjnwjjpnue8xmmpanz6csze6tcmtzzdtfsww7",
                crypto = new JCryptoData()
                {
                    argon2Params = new JArgon2Params()
                    {
                        salt = "0x22a9a6a23a93000f165c787d0b09bfdb"
                    },
                    cipherData = "0x7661b6e00a93aefd5606d454adca5763efd90ab04aea31ee3b2a036eb117b0d95c66a0d709aa9827ec18485f194283e5",
                    cipherName = "aes-256-gcm",
                    kdf = "argon2.IDKey",
                    nonce = "0x6a2708a6b5a40a683e6f35cc"
                },
                timestamp = 1657541141,
                version = 1
            };
        }

        public string Mnemonic { get; }
        public string Password { get; }
        public JKeyFile KeyFileJson { get; }
    }

    public class KeyFileTest : IClassFixture<KeyFileFixture>
    {
        public string Mnemonic { get; }
        public string Password { get; }
        public JKeyFile KeyFileJson { get; }

        public KeyFileTest(KeyFileFixture fixture)
        {
            Mnemonic = fixture.Mnemonic;
            Password = fixture.Password;
            KeyFileJson = fixture.KeyFileJson;
        }

        [Fact]
        public void When_Encrypt_ExpectToEqual()
        {
            // Setup
            var originalKeyStore = KeyStore.FromMnemonic(Mnemonic);

            // Execute
            var keyFile = KeyFile.Encrypt(originalKeyStore, Password);
            keyFile = new KeyFile(keyFile.ToJson());
            var decryptedKeyStore = keyFile.Decrypt(Password);

            // Validate
            originalKeyStore.Entropy.Should().BeEquivalentTo(decryptedKeyStore.Entropy);
            originalKeyStore.Mnemonic.Should().BeEquivalentTo(decryptedKeyStore.Mnemonic);
            originalKeyStore.Seed.Should().BeEquivalentTo(decryptedKeyStore.Seed);
        }

        [Fact]
        public void When_Decrypt_ExpectToEqual()
        {
            // Setup
            var originalKeyStore = KeyStore.FromMnemonic(Mnemonic);
            var keyFile = new KeyFile(KeyFileJson);

            // Execute
            KeyStore decryptedKeyStore = keyFile.Decrypt(Password);

            // Validate
            originalKeyStore.Entropy.Should().BeEquivalentTo(decryptedKeyStore.Entropy);
            originalKeyStore.Mnemonic.Should().BeEquivalentTo(decryptedKeyStore.Mnemonic);
            originalKeyStore.Seed.Should().BeEquivalentTo(decryptedKeyStore.Seed);
        }
    }
}
