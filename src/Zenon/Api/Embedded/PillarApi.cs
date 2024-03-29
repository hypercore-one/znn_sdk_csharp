﻿using System.Linq;
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
    public class PillarApi
    {
        public PillarApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<BigInteger> GetDepositedQsr(Address address)
        {
            return AmountUtils.ParseAmount(await
                Client.SendRequestAsync<string>("embedded.pillar.getDepositedQsr", address.ToString()));
        }

        public async Task<UncollectedReward> GetUncollectedReward(Address address)
        {
            var response = await Client.SendRequestAsync<JUncollectedReward>("embedded.pillar.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JRewardHistoryList>("embedded.pillar.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }

        public async Task<BigInteger> GetQsrRegistrationCost()
        {
            return AmountUtils.ParseAmount(await
                Client.SendRequestAsync<string>("embedded.pillar.getQsrRegistrationCost"));
        }

        public async Task<PillarInfoList> GetAll(uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JPillarInfoList>("embedded.pillar.getAll", pageIndex, pageSize);
            return new PillarInfoList(response);
        }

        public async Task<PillarInfo[]> GetByOwner(Address address)
        {
            var response = await Client.SendRequestAsync<JPillarInfo[]>("embedded.pillar.getByOwner", address.ToString());
            return response.Select(x => new PillarInfo(x)).ToArray();
        }

        public async Task<PillarInfo> GetByName(string name)
        {
            var response = await Client.SendRequestAsync<JPillarInfo>("embedded.pillar.getByName", name);
            return response != null ? new PillarInfo(response) : null;
        }

        public async Task<bool> CheckNameAvailability(string name)
        {
            return await Client.SendRequestAsync<bool>("embedded.pillar.checkNameAvailability", name);
        }

        public async Task<DelegationInfo> GetDelegatedPillar(Address address)
        {
            var response = await Client.SendRequestAsync<JDelegationInfo>("embedded.pillar.getDelegatedPillar", address.ToString());
            return response != null ? new DelegationInfo(response) : null;
        }

        public async Task<PillarEpochHistoryList> GetPillarEpochHistory(string name, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JPillarEpochHistoryList>("embedded.pillar.getPillarEpochHistory", name, pageIndex, pageSize);
            return new PillarEpochHistoryList(response);
        }

        public async Task<PillarEpochHistoryList> GetPillarsHistoryByEpoch(ulong epoch, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JPillarEpochHistoryList>("embedded.pillar.getPillarsHistoryByEpoch", epoch, pageIndex, pageSize);
            return new PillarEpochHistoryList(response);
        }

        // Contract methods
        public AccountBlockTemplate Register(string name, Address producerAddress, Address rewardAddress,
            int giveBlockRewardPercentage = 0, int giveDelegateRewardPercentage = 100)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.PillarAddress,
                TokenStandard.ZnnZts,
                Constants.PillarRegisterZnnAmount,
                Definitions.Pillar.EncodeFunction("Register",
                    name,
                    producerAddress,
                    rewardAddress,
                    giveBlockRewardPercentage,
                    giveDelegateRewardPercentage));
        }

        public AccountBlockTemplate RegisterLegacy(string name, Address producerAddress,
            Address rewardAddress, string publicKey, string signature,
            int giveBlockRewardPercentage = 0, int giveDelegateRewardPercentage = 100)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.PillarAddress,
                TokenStandard.ZnnZts,
                Constants.PillarRegisterZnnAmount,
                Definitions.Pillar.EncodeFunction("RegisterLegacy",
                    name,
                    producerAddress,
                    rewardAddress,
                    giveBlockRewardPercentage,
                    giveDelegateRewardPercentage,
                    publicKey,
                    signature));
        }

        public AccountBlockTemplate UpdatePillar(string name, Address producerAddress, Address rewardAddress,
            int giveBlockRewardPercentage, int giveDelegateRewardPercentage)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.PillarAddress,
                TokenStandard.ZnnZts,
                BigInteger.Zero,
                Definitions.Pillar.EncodeFunction("UpdatePillar",
                    name,
                    producerAddress,
                    rewardAddress,
                    giveBlockRewardPercentage,
                    giveDelegateRewardPercentage));
        }

        public AccountBlockTemplate Revoke(string name)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PillarAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Pillar.EncodeFunction("Revoke", name));
        }

        public AccountBlockTemplate Delegate(string name)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PillarAddress, TokenStandard.ZnnZts, BigInteger.Zero,
            Definitions.Pillar.EncodeFunction("Delegate", name));
        }

        public AccountBlockTemplate Undelegate()
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PillarAddress, TokenStandard.ZnnZts, BigInteger.Zero,
            Definitions.Pillar.EncodeFunction("Undelegate"));
        }

        // Common contract methods
        public AccountBlockTemplate CollectReward()
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PillarAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Common.EncodeFunction("CollectReward"));
        }

        public AccountBlockTemplate DepositQsr(BigInteger amount)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PillarAddress, TokenStandard.QsrZts, amount,
                Definitions.Common.EncodeFunction("DepositQsr"));
        }

        public AccountBlockTemplate WithdrawQsr()
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PillarAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Common.EncodeFunction("WithdrawQsr"));
        }
    }
}
