using FluentAssertions;
using System.Text;
using Xunit;
using Zenon.Utils;

namespace Zenon.Wallet
{
    public class KeyPairTest
    {
        public static readonly string MNEMONIC = 
            "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";
    
        public static readonly string MESSAGE = "Hello Zenon!";
        public static readonly string MESSAGE_SIGNATURE = "248591c0de1348336070564c2f1a43fad5529bf24fef9e4fe23298d55a2f59908bfa330b680ee170e6df3e5713bd7792f304620534cb9274b065052b84f5be0a";
        
        [Fact]
        public void When_SignMessage_ExpectToEqual()
        {
            // Setup
            var expectedSignature = BytesUtils.FromHexString(MESSAGE_SIGNATURE);
            var keyStore = KeyStore.FromMnemonic(MNEMONIC);
            var keyPair = keyStore.GetKeyPair();

            // Execute
            var actualSignature = keyPair.Sign(Encoding.UTF8.GetBytes(MESSAGE));

            // Validate
            expectedSignature.Should().BeEquivalentTo(actualSignature);
        }

        [Fact]
        public void When_VerifyMessage_ExpectValid()
        {
            // Setup
            var signature = BytesUtils.FromHexString(MESSAGE_SIGNATURE);
            var keyStore = KeyStore.FromMnemonic(MNEMONIC);
            var keyPair = keyStore.GetKeyPair();

            // Execute
            var sig = keyPair.Sign(Encoding.UTF8.GetBytes(MESSAGE));

            var valid = keyPair.Verify(signature, Encoding.UTF8.GetBytes(MESSAGE));

            // Validate
            valid.Should().BeTrue();
        }

        [Fact]
        public void When_VerifyMessage_ExpectInvalid()
        {
            // Setup
            var signature = BytesUtils.FromHexString(MESSAGE_SIGNATURE);
            var keyStore = KeyStore.NewRandom();
            var keyPair = keyStore.GetKeyPair();

            // Execute
            var valid = keyPair.Verify(signature, Encoding.UTF8.GetBytes(MESSAGE));

            // Validate
            valid.Should().BeFalse();
        }
    }
}
