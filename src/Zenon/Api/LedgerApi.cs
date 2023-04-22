using System;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.NoM;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Api
{
    public class LedgerApi
    {
        public LedgerApi(Lazy<IClient> client)
        {
            this.Client = client;
        }

        public Lazy<IClient> Client { get; }

        /// This method returns null if the account-block was accepted
        public async Task PublishRawTransaction(AccountBlockTemplate accountBlockTemplate)
        {
            await Client.Value.SendRequest("ledger.publishRawTransaction", accountBlockTemplate.ToJson());
        }

        public async Task<AccountBlockList> GetUnconfirmedBlocksByAddress(Address address, int pageIndex = 0, int pageSize = Constants.MemoryPoolPageSize)
        {
            var response = await Client.Value.SendRequest<JAccountBlockList>("ledger.getUnconfirmedBlocksByAddress", address.ToString(), pageIndex, pageSize);
            return new AccountBlockList(response);
        }

        public async Task<AccountBlockList> GetUnreceivedBlocksByAddress(Address address, int pageIndex = 0, int pageSize = Constants.MemoryPoolPageSize)
        {
            var response = await Client.Value.SendRequest<JAccountBlockList>("ledger.getUnreceivedBlocksByAddress", address.ToString(), pageIndex, pageSize);
            return new AccountBlockList(response);
        }

        // Blocks
        public async Task<AccountBlock> GetFrontierAccountBlock(Address address)
        {
            var response = await Client.Value.SendRequest<JAccountBlock>("ledger.getFrontierAccountBlock", address.ToString());
            return response != null ? new AccountBlock(response) : null;
        }

        public async Task<AccountBlock> GetAccountBlockByHash(Hash hash)
        {
            var response = await Client.Value.SendRequest<JAccountBlock>("ledger.getAccountBlockByHash", hash.ToString());
            return response != null ? new AccountBlock(response) : null;
        }

        public async Task<AccountBlockList> GetAccountBlocksByHeight(Address address, int height = 1, int count = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JAccountBlockList>("ledger.getAccountBlocksByHeight", address.ToString(), height, count);
            return new AccountBlockList(response);
        }

        /// pageIndex = 0 returns the most recent account blocks sorted descending by height
        public async Task<AccountBlockList> GetAccountBlocksByPage(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JAccountBlockList>("ledger.getAccountBlocksByPage", address.ToString(), pageIndex, pageSize);
            return new AccountBlockList(response);
        }

        // Momentum
        public async Task<Momentum> GetFrontierMomentum()
        {
            var response = await Client.Value.SendRequest<JMomentum>("ledger.getFrontierMomentum");
            return new Momentum(response);
        }

        public async Task<Momentum> GetMomentumBeforeTime(long time)
        {
            var response = await Client.Value.SendRequest<JMomentum>("ledger.getMomentumBeforeTime", time);
            return response != null ? new Momentum(response) : null;
        }

        public async Task<Momentum> GetMomentumByHash(Hash hash)
        {
            var response = await Client.Value.SendRequest<JMomentum>("ledger.getMomentumByHash", hash.ToString());
            return response != null ? new Momentum(response) : null;
        }

        public async Task<MomentumList> GetMomentumsByHeight(long height, long count)
        {
            height = height < 1 ? 1 : height;
            count = count > Constants.RpcMaxPageSize ? Constants.RpcMaxPageSize : count;
            var response = await Client.Value.SendRequest<JMomentumList>("ledger.getMomentumsByHeight", height, count);
            return new MomentumList(response);
        }

        /// pageIndex = 0 returns the most recent momentums sorted descending by height
        public async Task<MomentumList> GetMomentumsByPage(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JMomentumList>("ledger.getMomentumsByPage", pageIndex, pageSize);
            return new MomentumList(response);
        }

        public async Task<DetailedMomentumList> GetDetailedMomentumsByHeight(long height, long count)
        {
            height = height < 1 ? 1 : height;
            count = count > Constants.RpcMaxPageSize ? Constants.RpcMaxPageSize : count;
            var response = await Client.Value.SendRequest<JDetailedMomentumList>("ledger.getDetailedMomentumsByHeight", height, count);
            return new DetailedMomentumList(response);
        }

        public async Task<AccountInfo> GetAccountInfoByAddress(Address address)
        {
            var response = await Client.Value.SendRequest<JAccountInfo>("ledger.getAccountInfoByAddress", address.ToString());
            return new AccountInfo(response);
        }
    }
}
