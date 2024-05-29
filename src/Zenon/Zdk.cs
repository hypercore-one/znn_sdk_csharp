using System;
using System.Threading.Tasks;
using Zenon.Api;
using Zenon.Client;
using Zenon.Model.NoM;
using Zenon.Pow;
using Zenon.Utils;
using Zenon.Wallet;

namespace Zenon
{
    public sealed class Zdk
    {
        public Zdk(IClient client)
        {
            Client = client;
            Ledger = new LedgerApi(Client);
            Stats = new StatsApi(Client);
            Embedded = new EmbeddedApi(Client);
            Subscribe = new SubscribeApi(Client);
        }

        public IWalletAccount DefaultWalletAccount { get; set; }

        public IClient Client { get; }
        public LedgerApi Ledger { get; }
        public StatsApi Stats { get; }
        public EmbeddedApi Embedded { get; }
        public SubscribeApi Subscribe { get; }

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
                throw new ZdkException("No default wallet account selected");

            return await BlockUtils.SendAsync(this, transaction, account, generatingPowCallback ?? delegate { }, waitForRequiredPlasma);
        }

        public async Task<bool> RequiresPoWAsync(AccountBlockTemplate transaction)
        {
            return await RequiresPoWAsync(transaction, DefaultWalletAccount);
        }

        public async Task<bool> RequiresPoWAsync(AccountBlockTemplate transaction, IWalletAccount currentAccount)
        {
            var account = currentAccount ?? DefaultWalletAccount;

            if (account == null)
                throw new ZdkException("No default wallet account selected");

            return await BlockUtils.RequiresPoWAsync(this, transaction, account);
        }
    }
}