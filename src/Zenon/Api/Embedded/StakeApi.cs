using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class StakeApi
    {
        public StakeApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<StakeList> GetEntriesByAddress(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JStakeList>("embedded.stake.getEntriesByAddress", address.ToString(), pageIndex, pageSize);
            return new StakeList(response);
        }

        public async Task<UncollectedReward> GetUncollectedReward(Address address)
        {
            var response = await Client.SendRequest<JUncollectedReward>("embedded.stake.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JRewardHistoryList>("embedded.stake.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }
    }
}
