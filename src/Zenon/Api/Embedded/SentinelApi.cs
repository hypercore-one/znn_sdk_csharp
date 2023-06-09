using System;
using System.Numerics;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Api.Embedded
{
    public class SentinelApi
    {
        public SentinelApi(Lazy<IClient> client)
        {
            Client = client;
        }

        public Lazy<IClient> Client { get; }

        public async Task<SentinelInfoList> GetAllActive(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JSentinelInfoList>("embedded.sentinel.getAllActive", pageIndex, pageSize);
            return new SentinelInfoList(response);
        }

        public async Task<SentinelInfo> GetByOwner(Address owner)
        {
            var response = await Client.Value.SendRequest<JSentinelInfo>("embedded.sentinel.getByOwner", owner.ToString());
            return response != null ? new SentinelInfo(response) : null;
        }

        public async Task<BigInteger> GetDepositedQsr(Address address)
        {
            return AmountUtils.ParseAmount(await 
                Client.Value.SendRequest<string>("embedded.sentinel.getDepositedQsr", address.ToString()));
        }

        public async Task<UncollectedReward> GetUncollectedReward(Address address)
        {
            var response = await Client.Value.SendRequest<JUncollectedReward>("embedded.sentinel.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JRewardHistoryList>("embedded.sentinel.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }

        // Contract methods
        public AccountBlockTemplate Register()
        {
            return AccountBlockTemplate.CallContract(Address.SentinelAddress, TokenStandard.ZnnZts, Constants.SentinelRegisterZnnAmount,
                Definitions.Sentinel.EncodeFunction("Register"));
        }

        public AccountBlockTemplate Revoke()
        {
            return AccountBlockTemplate.CallContract(Address.SentinelAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Sentinel.EncodeFunction("Revoke"));
        }

        // Common contract methods
        public AccountBlockTemplate CollectReward()
        {
            return AccountBlockTemplate.CallContract(Address.SentinelAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Common.EncodeFunction("CollectReward"));
        }

        public AccountBlockTemplate DepositQsr(BigInteger amount)
        {
            return AccountBlockTemplate.CallContract(Address.SentinelAddress, TokenStandard.QsrZts, amount,
                Definitions.Common.EncodeFunction("DepositQsr"));
        }

        public AccountBlockTemplate WithdrawQsr()
        {
            return AccountBlockTemplate.CallContract(Address.SentinelAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Common.EncodeFunction("WithdrawQsr"));
        }
    }
}