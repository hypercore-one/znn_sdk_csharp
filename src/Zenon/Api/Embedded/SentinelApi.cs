using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class SentinelApi
    {
        public SentinelApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<SentinelInfoList> GetAllActive(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JSentinelInfoList>("embedded.sentinel.getAllActive", pageIndex, pageSize);
            return new SentinelInfoList(response);
        }

        public async Task<SentinelInfo> GetByOwner(Address owner)
        {
            var response = await Client.SendRequest<JSentinelInfo>("embedded.sentinel.getByOwner", owner.ToString());
            return response != null ? new SentinelInfo(response) : null;
        }

        public async Task<long> GetDepositedQsr(Address address)
        {
            return await Client.SendRequest<long>("embedded.sentinel.getDepositedQsr", address.ToString());
        }

        public async Task<UncollectedReward> GetUncollectedReward(Address address)
        {
            var response = await Client.SendRequest<JUncollectedReward>("embedded.sentinel.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JRewardHistoryList>("embedded.sentinel.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }
    }
}