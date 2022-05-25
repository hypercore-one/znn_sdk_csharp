using System;
using System.IO;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.NoM;
using Zenon.Pow;
using Zenon.Utils;
using Zenon.Wallet;

namespace Zenon
{
    public class Znn
    {
        public static readonly Znn Instance = new Znn();

        public KeyPair DefaultKeyPair { get; set; }
        public KeyStore DefaultKeyStore { get; set; }
        public string DefaultKeyStorePath { get; set; }

        public Lazy<IClient> Client { get; }
        public KeyStoreManager KeyStoreManager { get; }

        public Api.LedgerApi Ledger { get; }
        public Api.StatsApi Stats { get; }
        public Api.EmbeddedApi Embedded { get; }
        public Api.SubscribeApi Subscribe { get; }

        private Znn()
        {
            KeyStoreManager = new KeyStoreManager(Constants.ZnnDefaultWalletDirectory);
            Client = new Lazy<IClient>(() => new WsClient());
            Ledger = new Api.LedgerApi(Client);
            Stats = new Api.StatsApi(Client);
            Embedded = new Api.EmbeddedApi(Client);
            Subscribe = new Api.SubscribeApi(Client);
        }

        public async Task<AccountBlockTemplate> Send(AccountBlockTemplate transaction, bool waitForRequiredPlasma = false)
        {
            return await Send(transaction, DefaultKeyPair, delegate { }, waitForRequiredPlasma);
        }

        public async Task<AccountBlockTemplate> Send(AccountBlockTemplate transaction,
            Action<PowStatus> generatingPowCallback, bool waitForRequiredPlasma = false)
        {
            return await Send(transaction, DefaultKeyPair, generatingPowCallback, waitForRequiredPlasma);
        }

        public async Task<AccountBlockTemplate> Send(AccountBlockTemplate transaction,
            KeyPair currentKeyPair, Action<PowStatus> generatingPowCallback, bool waitForRequiredPlasma = false)
        {
            var keypair = currentKeyPair ?? DefaultKeyPair;

            if (keypair == null)
                throw new ZnnSdkException("No default keyPair selected");

            return await BlockUtils.Send(transaction, keypair, generatingPowCallback, waitForRequiredPlasma);
        }

        public async Task<bool> RequiresPoW(AccountBlockTemplate transaction)
        {
            return await RequiresPoW(transaction, DefaultKeyPair);
        }

        public async Task<bool> RequiresPoW(AccountBlockTemplate transaction, KeyPair currentKeyPair)
        {
            var keypair = currentKeyPair ?? DefaultKeyPair;

            return await BlockUtils.RequiresPoW(transaction, keypair);
        }
    }
}
