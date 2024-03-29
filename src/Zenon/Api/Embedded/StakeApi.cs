﻿using System;
using System.Numerics;
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
        public StakeApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<StakeList> GetEntriesByAddress(Address address, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JStakeList>("embedded.stake.getEntriesByAddress", address.ToString(), pageIndex, pageSize);
            return new StakeList(response);
        }

        public async Task<UncollectedReward> GetUncollectedReward(Address address)
        {
            var response = await Client.SendRequestAsync<JUncollectedReward>("embedded.stake.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JRewardHistoryList>("embedded.stake.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }

        // Contract methods
        public AccountBlockTemplate Stake(long durationInSec, BigInteger amount)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.StakeAddress, TokenStandard.ZnnZts, amount,
                Definitions.Stake.EncodeFunction("Stake", durationInSec));
        }

        public AccountBlockTemplate Cancel(Hash id)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.StakeAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Stake.EncodeFunction("Cancel", id.Bytes));
        }

        // Common contract methods
        public AccountBlockTemplate CollectReward()
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.StakeAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Common.EncodeFunction("CollectReward"));
        }
    }
}
