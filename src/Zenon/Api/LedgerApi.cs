using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.NoM;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Api
{
    public class LedgerApi
    {
        public LedgerApi(IClient client)
        {
            this.Client = client;
        }

        public IClient Client { get; }

        /// This method returns null if the account-block was accepted
        public async Task PublishRawTransaction(AccountBlockTemplate accountBlockTemplate)
        {
            await Client.SendRequestAsync("ledger.publishRawTransaction", accountBlockTemplate.ToJson());
        }

        public async Task<AccountBlockList> GetUnconfirmedBlocksByAddress(Address address, uint pageIndex = 0, uint pageSize = Constants.MemoryPoolPageSize)
        {
            var response = await Client.SendRequestAsync<JAccountBlockList>("ledger.getUnconfirmedBlocksByAddress", address.ToString(), pageIndex, pageSize);
            return new AccountBlockList(response);
        }

        public async Task<AccountBlockList> GetUnreceivedBlocksByAddress(Address address, uint pageIndex = 0, uint pageSize = Constants.MemoryPoolPageSize)
        {
            var response = await Client.SendRequestAsync<JAccountBlockList>("ledger.getUnreceivedBlocksByAddress", address.ToString(), pageIndex, pageSize);
            return new AccountBlockList(response);
        }

        // Blocks
        public async Task<AccountBlock> GetFrontierAccountBlock(Address address)
        {
            var response = await Client.SendRequestAsync<JAccountBlock>("ledger.getFrontierAccountBlock", address.ToString());
            return response != null ? new AccountBlock(response) : null;
        }

        public async Task<AccountBlock> GetAccountBlockByHash(Hash hash)
        {
            var response = await Client.SendRequestAsync<JAccountBlock>("ledger.getAccountBlockByHash", hash.ToString());
            return response != null ? new AccountBlock(response) : null;
        }

        public async Task<AccountBlockList> GetAccountBlocksByHeight(Address address, ulong height = 1, ulong count = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JAccountBlockList>("ledger.getAccountBlocksByHeight", address.ToString(), height, count);
            return new AccountBlockList(response);
        }

        /// pageIndex = 0 returns the most recent account blocks sorted descending by height
        public async Task<AccountBlockList> GetAccountBlocksByPage(Address address, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JAccountBlockList>("ledger.getAccountBlocksByPage", address.ToString(), pageIndex, pageSize);
            return new AccountBlockList(response);
        }

        // Momentum
        public async Task<Momentum> GetFrontierMomentum()
        {
            var response = await Client.SendRequestAsync<JMomentum>("ledger.getFrontierMomentum");
            return new Momentum(response);
        }

        public async Task<Momentum> GetMomentumBeforeTime(long time)
        {
            var response = await Client.SendRequestAsync<JMomentum>("ledger.getMomentumBeforeTime", time);
            return response != null ? new Momentum(response) : null;
        }

        public async Task<Momentum> GetMomentumByHash(Hash hash)
        {
            var response = await Client.SendRequestAsync<JMomentum>("ledger.getMomentumByHash", hash.ToString());
            return response != null ? new Momentum(response) : null;
        }

        public async Task<MomentumList> GetMomentumsByHeight(ulong height, ulong count)
        {
            height = height < 1 ? 1 : height;
            count = count > Constants.RpcMaxPageSize ? Constants.RpcMaxPageSize : count;
            var response = await Client.SendRequestAsync<JMomentumList>("ledger.getMomentumsByHeight", height, count);
            return new MomentumList(response);
        }

        /// pageIndex = 0 returns the most recent momentums sorted descending by height
        public async Task<MomentumList> GetMomentumsByPage(uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JMomentumList>("ledger.getMomentumsByPage", pageIndex, pageSize);
            return new MomentumList(response);
        }

        public async Task<DetailedMomentumList> GetDetailedMomentumsByHeight(ulong height, ulong count)
        {
            height = height < 1 ? 1 : height;
            count = count > Constants.RpcMaxPageSize ? Constants.RpcMaxPageSize : count;
            var response = await Client.SendRequestAsync<JDetailedMomentumList>("ledger.getDetailedMomentumsByHeight", height, count);
            return new DetailedMomentumList(response);
        }

        public async Task<AccountInfo> GetAccountInfoByAddress(Address address)
        {
            var response = await Client.SendRequestAsync<JAccountInfo>("ledger.getAccountInfoByAddress", address.ToString());
            return new AccountInfo(response);
        }
    }
}
