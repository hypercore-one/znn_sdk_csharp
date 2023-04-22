using System;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class StakeApi
    {
        public StakeApi(Lazy<IClient> client)
        {
            Client = client;
        }

        public Lazy<IClient> Client { get; }

        public async Task<StakeList> GetEntriesByAddress(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JStakeList>("embedded.stake.getEntriesByAddress", address.ToString(), pageIndex, pageSize);
            return new StakeList(response);
        }

        public async Task<UncollectedReward> GetUncollectedReward(Address address)
        {
            var response = await Client.Value.SendRequest<JUncollectedReward>("embedded.stake.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JRewardHistoryList>("embedded.stake.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }

        // Contract methods
        public AccountBlockTemplate Stake(long durationInSec, long amount)
        {
            return AccountBlockTemplate.CallContract(Address.StakeAddress, TokenStandard.ZnnZts, amount,
                Definitions.Stake.EncodeFunction("Stake", durationInSec));
        }

        public AccountBlockTemplate Cancel(Hash id)
        {
            return AccountBlockTemplate.CallContract(Address.StakeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Stake.EncodeFunction("Cancel", id.Bytes));
        }

        // Common contract methods
        public AccountBlockTemplate CollectReward()
        {
            return AccountBlockTemplate.CallContract(Address.StakeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Common.EncodeFunction("CollectReward"));
        }
    }
}
