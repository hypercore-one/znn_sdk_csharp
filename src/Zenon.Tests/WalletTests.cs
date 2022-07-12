using FluentAssertions;
using Xunit;
using Zenon.Tests.TestData.Wallet;
using Zenon.Wallet;

namespace Zenon.Tests
{
    public class WalletTests
    {
        public const string MNEMONIC = 
            "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";
        public const string ENTROPY = "bc827d0a00a72354dce4c44a59485288500b49382f9ba88a016351787b7b15ca";
        public const string SEED = "19f1d107d49f42ebc14d46b51001c731569f142590fdd20167ddeedbb201516731ad5ac9b58d3a1c9c09debfe62538379461e4ea9f038124c428784fecc645b7";

        [Fact]
        public void Validate_KeyStore_FromMnemonic()
        {
            // Execute
            KeyStore keyStore = KeyStore.FromMnemonic(MNEMONIC);

            // Validate
            keyStore.Mnemonic.Should().Be(MNEMONIC);
            keyStore.Entropy.Should().Be(ENTROPY);
            keyStore.Seed.Should().Be(SEED);
        }

        [Fact]
        public void Validate_KeyStore_FromEntropy()
        {
            // Execute
            KeyStore keyStore = KeyStore.FromEntropy(ENTROPY);

            // Validate
            keyStore.Mnemonic.Should().Be(MNEMONIC);
            keyStore.Entropy.Should().Be(ENTROPY);
            keyStore.Seed.Should().Be(SEED);
        }

        [Fact]
        public void Validate_KeyStore_FromSeed()
        {
            // Execute
            KeyStore keyStore = KeyStore.FromSeed(SEED);

            // Validate
            keyStore.Mnemonic.Should().Be(null);
            keyStore.Entropy.Should().Be(null);
            keyStore.Seed.Should().Be(SEED);
        }

        [Theory]
        [ClassData(typeof(KeyStoreDerivationAccountTestData))]
        public void When_DeriveAccount_ExpectToEqual(KeyStore keyStore, int index, KeyPair expectedResult)
        {
            // Execute
            KeyPair result = keyStore.GetKeyPair(index);

            // Validate
            expectedResult.PrivateKey.Should().BeEquivalentTo(result.PrivateKey);
            expectedResult.PublicKey.Should().BeEquivalentTo(result.PublicKey);
            expectedResult.Address.Should().BeEquivalentTo(result.Address);
        }
    }
}
