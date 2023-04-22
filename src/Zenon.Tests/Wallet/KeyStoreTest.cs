using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Wallet
{
    public class KeyStoreTest
    {
        public const string MNEMONIC =
            "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";
        public const string ENTROPY = "bc827d0a00a72354dce4c44a59485288500b49382f9ba88a016351787b7b15ca";
        public const string SEED = "19f1d107d49f42ebc14d46b51001c731569f142590fdd20167ddeedbb201516731ad5ac9b58d3a1c9c09debfe62538379461e4ea9f038124c428784fecc645b7";

        public static IEnumerable<object[]> KeyStoreDerivationAccountTestData()
        {
            var keyStore = KeyStore.FromMnemonic(MNEMONIC);

            yield return new object[]
            {
                    keyStore,
                    0,
                    new KeyPair(
                        BytesUtils.FromHexString("d6b01f96b566d7df9b5b53b1971e4baeb74cc64167a9843f82d04b2194ca4863"),
                        BytesUtils.FromHexString("3e13d7238d0e768a567dce84b54915f2323f2dcd0ef9a716d9c61abed631ba10"),
                        Address.Parse("z1qqjnwjjpnue8xmmpanz6csze6tcmtzzdtfsww7"))
            };

            yield return new object[]
                {
                    keyStore,
                    1,
                    new KeyPair(
                        BytesUtils.FromHexString("bd14c955a2e67246dd8f273127a124ef97b869ef1301378c44760f96b426ee18"),
                        BytesUtils.FromHexString("fb6416d170dda0b2a2857d8460f746c9639522cf2255ed2efcd54f6337bd718e"),
                        Address.Parse("z1qr44l6ajstm5gfrvwtsrfg446y6mcv8r60v090"))
                };

            yield return new object[]
                {
                    keyStore,
                    2,
                    new KeyPair(
                        BytesUtils.FromHexString("1fc9a73b1838f29f86f9221e51051020b16a81034f92950cb8d486d04f12de37"),
                        BytesUtils.FromHexString("e253210b1bb64a9f3a606cacc988bc22f123c590507e3b8a076ec3923f41d6c1"),
                        Address.Parse("z1qqk6gsd2sv49azqrzxqe8q6jjssufa35mry83t"))
                };

            yield return new object[]
                {
                    keyStore,
                    3,
                    new KeyPair(
                        BytesUtils.FromHexString("67f9700dc8bd0b61c2ae76f115655f6f06a27b8fad38ec98e562911d0859bdd8"),
                        BytesUtils.FromHexString("124a2d88293f098d3b37de80e58bd5fbc3b584a5169354983a70dd8a9bd7c56e"),
                        Address.Parse("z1qr5kzrlrw4e4stkcylcynwv5rja8ksh6qvm2ul"))
                };

            yield return new object[]
                {
                    keyStore,
                    4,
                    new KeyPair(
                        BytesUtils.FromHexString("97fc1b2fd3daedd9f38bb5f11cf22d6cc767a2e976b4c9422b7346a68a62a7e9"),
                        BytesUtils.FromHexString("6fd663e4c66201718611d26d0633381f07c23f6a08862e07c0b718e090dc1366"),
                        Address.Parse("z1qq8gh5vdezsara9p2t7p0m7dvplm2sjzq0nr3d"))
                };
        }

        [Fact]
        public void Validate_KeyStore_FromMnemonic()
        {
            // Execute
            var keyStore = KeyStore.FromMnemonic(MNEMONIC);

            // Validate
            keyStore.Mnemonic.Should().Be(MNEMONIC);
            keyStore.Entropy.Should().Be(ENTROPY);
            keyStore.Seed.Should().Be(SEED);
        }

        [Fact]
        public void Validate_KeyStore_FromEntropy()
        {
            // Execute
            var keyStore = KeyStore.FromEntropy(ENTROPY);

            // Validate
            keyStore.Mnemonic.Should().Be(MNEMONIC);
            keyStore.Entropy.Should().Be(ENTROPY);
            keyStore.Seed.Should().Be(SEED);
        }

        [Fact]
        public void Validate_KeyStore_FromSeed()
        {
            // Execute
            var keyStore = KeyStore.FromSeed(SEED);

            // Validate
            keyStore.Mnemonic.Should().Be(null);
            keyStore.Entropy.Should().Be(null);
            keyStore.Seed.Should().Be(SEED);
        }

        [Theory]
        [MemberData(nameof(KeyStoreDerivationAccountTestData))]
        public void When_DeriveAccount_ExpectToEqual(KeyStore keyStore, int index, KeyPair expectedResult)
        {
            // Execute
            var result = keyStore.GetKeyPair(index);

            // Validate
            expectedResult.PrivateKey.Should().BeEquivalentTo(result.PrivateKey);
            expectedResult.PublicKey.Should().BeEquivalentTo(result.PublicKey);
            expectedResult.Address.Should().BeEquivalentTo(result.Address);
        }

        [Fact]
        public void When_DeriveAddressByRange_ExpectResult()
        {
            // Setup
            var expectedResult = new Address[] {
                Address.Parse("z1qr44l6ajstm5gfrvwtsrfg446y6mcv8r60v090"),
                Address.Parse("z1qqk6gsd2sv49azqrzxqe8q6jjssufa35mry83t"),
                Address.Parse("z1qr5kzrlrw4e4stkcylcynwv5rja8ksh6qvm2ul")
            };

            var keyStore = KeyStore.FromMnemonic(MNEMONIC);

            // Execute
            var result = keyStore.DeriveAddressesByRange(1, 4);

            // Validate
            expectedResult.Should().BeEquivalentTo(result);
        }

        [Fact]
        public void When_FindAddress_ExpectResult()
        {
            // Setup
            var keyStore = KeyStore.FromMnemonic(MNEMONIC);

            // Execute
            var result = keyStore.FindAddress(Address.Parse("z1qq8gh5vdezsara9p2t7p0m7dvplm2sjzq0nr3d"), 10);

            // Validate
            result.Should().NotBeNull();
        }
    }
}
