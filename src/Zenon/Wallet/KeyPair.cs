using System;
using System.Threading;
using System.Threading.Tasks;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Wallet
{
    public class KeyPair : IWalletAccount
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

        public byte[] SignTx(AccountBlockTemplate tx)
        {
            return Crypto.Crypto.Sign(tx.Hash.Bytes, this.PrivateKey, this.PublicKey);
        }

        public bool Verify(byte[] signature, byte[] message)
        {
            return Crypto.Crypto.Verify(signature, message, this.PublicKey);
        }

        public byte[] GeneratePublicKey(byte[] privateKey)
        {
            return Crypto.Crypto.GetPublicKey(privateKey);
        }

        public async Task<Address> GetAddressAsync(CancellationToken token = default)
        {
            return await Task.Run(() => this.Address);
        }

        public async Task<byte[]> GetPublicKeyAsync(CancellationToken token = default)
        {
            return await Task.Run(() => this.PublicKey);
        }

        public async Task<byte[]> SignAsync(byte[] message, CancellationToken token = default)
        {
            return await Task.Run(() => this.Sign(message));
        }

        public async Task<byte[]> SignTxAsync(AccountBlockTemplate tx, CancellationToken token = default)
        {
            return await Task.Run(() => this.SignTx(tx));
        }
    }
}