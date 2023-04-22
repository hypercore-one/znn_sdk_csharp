using System;
using Zenon.Model.Primitives;

namespace Zenon.Wallet
{
    public class KeyPair
    {
        public KeyPair(byte[] privateKey, byte[] publicKey = null, Address address = null)
        {
            if (privateKey == null)
                throw new ArgumentNullException(nameof(privateKey));

            this.PrivateKey = privateKey;
            this.PublicKey = publicKey ?? Crypto.Crypto.GetPublicKey(this.PrivateKey);
            this.Address = address ?? Address.FromPublicKey(this.PublicKey);
        }

        public byte[] PrivateKey { get; }

        public byte[] PublicKey { get; }

        public Address Address { get; }

        public byte[] Sign(byte[] message)
        {
            return Crypto.Crypto.Sign(message, this.PrivateKey, this.PublicKey);
        }

        public bool Verify(byte[] signature, byte[] message)
        {
            return Crypto.Crypto.Verify(signature, message, this.PublicKey);
        }

        public byte[] GeneratePublicKey(byte[] privateKey)
        {
            return Crypto.Crypto.GetPublicKey(privateKey);
        }
    }
}