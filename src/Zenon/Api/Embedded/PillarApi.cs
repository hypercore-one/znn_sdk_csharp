using System.Linq;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class PillarApi
    {
        public PillarApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<long> GetDepositedQsr(Address address)
        {
            return await Client.SendRequest<long>("embedded.pillar.getDepositedQsr", address.ToString());
        }

        public async Task<UncollectedReward> GetUncollectedReward(Address address)
        {
            var response = await Client.SendRequest<JUncollectedReward>("embedded.pillar.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JRewardHistoryList>("embedded.pillar.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }

        public async Task<long> GetQsrRegistrationCost()
        {
            return await Client.SendRequest<long>("embedded.pillar.getQsrRegistrationCost");
        }

        public async Task<PillarInfoList> GetAll(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JPillarInfoList>("embedded.pillar.getAll", pageIndex, pageSize);
            return new PillarInfoList(response);
        }

        public async Task<PillarInfo[]> GetByOwner(Address address)
        {
            var response = await Client.SendRequest<JPillarInfo[]>("embedded.pillar.getByOwner", address.ToString());
            return response.Select(x => new PillarInfo(x)).ToArray();
        }

        public async Task<PillarInfo> GetByName(string name)
        {
            var response = await Client.SendRequest<JPillarInfo>("embedded.pillar.getByName", name);
            return response != null ? new PillarInfo(response) : null;
        }

        public async Task<bool> CheckNameAvailability(string name)
        {
            return await Client.SendRequest<bool>("embedded.pillar.checkNameAvailability", name);
        }

        public async Task<DelegationInfo> GetDelegatedPillar(Address address)
        {
            var response = await Client.SendRequest<JDelegationInfo>("embedded.pillar.getDelegatedPillar", address.ToString());
            return response != null ? new DelegationInfo(response) : null;
        }

        public async Task<PillarEpochHistoryList> GetPillarEpochHistory(string name, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JPillarEpochHistoryList>("embedded.pillar.getPillarEpochHistory", name, pageIndex, pageSize);
            return new PillarEpochHistoryList(response);
        }

        public async Task<PillarEpochHistoryList> GetPillarsHistoryByEpoch(int epoch, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JPillarEpochHistoryList>("embedded.pillar.getPillarsHistoryByEpoch", epoch, pageIndex, pageSize);
            return new PillarEpochHistoryList(response);
        }

        // Contract methods
        public AccountBlockTemplate Register(string name, Address producerAddress, Address rewardAddress,
            int giveBlockRewardPercentage = 0, int giveDelegateRewardPercentage = 100)
        {
            return AccountBlockTemplate.CallContract(
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
            return AccountBlockTemplate.CallContract(
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
            return AccountBlockTemplate.CallContract(
                Address.PillarAddress,
                TokenStandard.ZnnZts,
                0,
                Definitions.Pillar.EncodeFunction("UpdatePillar",
                    name,
                    producerAddress,
                    rewardAddress,
                    giveBlockRewardPercentage,
                    giveDelegateRewardPercentage));
        }

        public AccountBlockTemplate Revoke(string name)
        {
            return AccountBlockTemplate.CallContract(Address.PillarAddress, TokenStandard.ZnnZts, 0,
                Definitions.Pillar.EncodeFunction("Revoke", name));
        }

        public AccountBlockTemplate Delegate(string name)
        {
            return AccountBlockTemplate.CallContract(Address.PillarAddress, TokenStandard.ZnnZts, 0,
            Definitions.Pillar.EncodeFunction("Delegate", name));
        }

        public AccountBlockTemplate Undelegate()
        {
            return AccountBlockTemplate.CallContract(Address.PillarAddress, TokenStandard.ZnnZts, 0,
            Definitions.Pillar.EncodeFunction("Undelegate"));
        }

        // Common contract methods
        public AccountBlockTemplate CollectReward()
        {
            return AccountBlockTemplate.CallContract(Address.PillarAddress, TokenStandard.ZnnZts, 0,
                Definitions.Common.EncodeFunction("CollectReward"));
        }

        public AccountBlockTemplate DepositQsr(long amount)
        {
            return AccountBlockTemplate.CallContract(Address.PillarAddress, TokenStandard.QsrZts, amount,
                Definitions.Common.EncodeFunction("DepositQsr"));
        }

        public AccountBlockTemplate WithdrawQsr()
        {
            return AccountBlockTemplate.CallContract(Address.PillarAddress, TokenStandard.ZnnZts, 0,
                Definitions.Common.EncodeFunction("WithdrawQsr"));
        }
    }
}
