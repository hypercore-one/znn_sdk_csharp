using System;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.NoM;
using Zenon.Pow;
using Zenon.Utils;
using Zenon.Wallet;

namespace Zenon
{
    public sealed class Znn
    {
        public static readonly Znn Mainnet = new Znn(Constants.ChainId);

        public Znn(int chainIdentifier)
        {
            ChainIdentifier = chainIdentifier;
            Client = new Lazy<IClient>(() => new WsClient());
            Ledger = new Api.LedgerApi(Client);
            Stats = new Api.StatsApi(Client);
            Embedded = new Api.EmbeddedApi(Client);
            Subscribe = new Api.SubscribeApi(Client);
        }

        public IWalletAccount DefaultWalletAccount { get; set; }

        public int ChainIdentifier { get; }
        public Lazy<IClient> Client { get; }
        public Api.LedgerApi Ledger { get; }
        public Api.StatsApi Stats { get; }
        public Api.EmbeddedApi Embedded { get; }
        public Api.SubscribeApi Subscribe { get; }

        public async Task<AccountBlockTemplate> SendAsync(AccountBlockTemplate transaction,
            Action<PowStatus> generatingPowCallback = default, bool waitForRequiredPlasma = false)
        {
            return await SendAsync(transaction, DefaultWalletAccount, generatingPowCallback, waitForRequiredPlasma);
        }

        public async Task<AccountBlockTemplate> SendAsync(AccountBlockTemplate transaction,
            IWalletAccount currentAccount, Action<PowStatus> generatingPowCallback = default, bool waitForRequiredPlasma = false)
        {
            var account = currentAccount ?? DefaultWalletAccount;

            if (account == null)
                throw new ZnnSdkException("No default wallet account selected");

            return await BlockUtils.SendAsync(this, transaction, account, generatingPowCallback, waitForRequiredPlasma);
        }

        public async Task<bool> RequiresPoWAsync(AccountBlockTemplate transaction)
        {
            return await RequiresPoWAsync(transaction, DefaultWalletAccount);
        }

        public async Task<bool> RequiresPoWAsync(AccountBlockTemplate transaction, IWalletAccount currentAccount)
        {
            var account = currentAccount ?? DefaultWalletAccount;

            if (account == null)
                throw new ZnnSdkException("No default wallet account selected");

            return await BlockUtils.RequiresPoWAsync(this, transaction, account);
        }
    }
}